using BfmeFoundationProject.HttpInstruments;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    public static class BfmeWorkshopScriptManager
    {
        public static async Task<(Dictionary<string, bool> requirements, string files_directory)> RunIfScripted(BfmeWorkshopEntry entry)
        {
            if (!entry.Files.Any(x => x.Name == "package.wps") || entry.Type != 1)
                return ([], "");

            var script = await HttpMarshal.GetString(url: entry.Files.First(x => x.Name == "package.wps").Url, headers: []);
            string files_directory = "";
            var variables = new Dictionary<string, string>();
            var requirements = new Dictionary<string, bool>();

            var i = 0;
            foreach (var line in script.Replace("\r", "").Split("\n"))
            {
                i++;

                if (line.StartsWith("//"))
                    continue;

                var tokens = new List<string>();
                var token = "";
                foreach (char c in line)
                {
                    token += c;

                    if (token.Length <= 1)
                        continue;

                    if (token.StartsWith("\""))
                    {
                        if (c == '"')
                        {
                            foreach (var variable in variables) token = token.Replace($"{{{variable.Key}}}", variable.Value);
                            tokens.Add(token.Trim('"'));
                            token = "";
                        }
                    }
                    else
                    {
                        if (c == ' ')
                        {
                            if (token.Trim(' ') != "") tokens.Add(token.Trim(' '));
                            token = "";
                        }
                    }
                }

                if (token.StartsWith('"'))
                    throw new BfmeWorkshopScriptSyntaxErrorException("String terminator expected.");
                else if (token.Trim(' ') != "")
                    tokens.Add(token.Trim(' '));

                if (tokens.Count == 0)
                    continue;

                try
                {
                    if (tokens[0] == "require")
                    {
                        var requirement_name = VerifyToken(1, "requirement name");
                        EnforceKeyword(2, "if");
                        var comparison_lhs = VerifyToken(3, "requirement comparison left hand side expression");
                        var comparison_condition = VerifyToken(4, "requirement comparison mode");
                        var comparison_rhs = VerifyToken(5, "requirement comparison right hand side expression");
                        var comparison_result = false;

                        if (comparison_condition == "equals")
                            comparison_result = comparison_lhs == comparison_rhs;
                        else if (comparison_condition == "!equals")
                            comparison_result = comparison_lhs != comparison_rhs;
                        else if (comparison_condition == "contains")
                            comparison_result = comparison_lhs.Contains(comparison_rhs);
                        else if (comparison_condition == "!contains")
                            comparison_result = !comparison_lhs.Contains(comparison_rhs);
                        else
                            throw new BfmeWorkshopScriptSyntaxErrorException("Invalid requirement comparison mode.");

                        requirements.Add(requirement_name, !comparison_result && !requirements.Any(x => !x.Value));
                    }
                    else if (tokens[0] == "files")
                    {
                        EnforceKeyword(1, "be");
                        EnforceKeyword(2, "from");
                        files_directory = VerifyToken(3, "files source expression");

                        entry.Files.RemoveAll(x => x.Name != "package.wps");
                    }
                    else if (tokens[0] == "let")
                    {
                        var variable_name = VerifyToken(1, "variable name");
                        EnforceKeyword(2, "be");
                        var variable_value = "";

                        if (tokens.Count > 4)
                        {
                            var variable_selector = VerifyToken(3, "variable value expression key selector");
                            EnforceKeyword(4, "from");
                            var variable_source = VerifyToken(5, "variable value expression source selector");

                            try
                            {
                                if (variable_selector == "all")
                                {
                                    if (variable_source.StartsWith(@"HKLM\"))
                                    {
                                        using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(variable_source.Replace(@"HKLM\", "").Replace(@"SOFTWARE\", @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\"), false);
                                        variable_value = string.Join("\n", registryKey.GetValueNames().Select(x => $"{x} = {registryKey.GetValue(x)}"));
                                    }
                                    else if (variable_source.Contains(@":\"))
                                    {
                                        variable_value = FileUtils.ReadText(variable_source, "");
                                    }
                                }
                                else
                                {
                                    if (variable_source.StartsWith(@"HKLM\"))
                                    {
                                        using RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(variable_source.Replace(@"HKLM\", "").Replace(@"SOFTWARE\", @$"SOFTWARE\{(nint.Size == 8 ? "WOW6432Node" : "")}\"), false);
                                        variable_value = registryKey.GetValue(variable_selector) as string;
                                    }
                                    else if (variable_source.Contains(@":\"))
                                    {
                                        variable_value = FileUtils.ReadText(variable_source, "");

                                        if (Path.GetExtension(variable_source) == ".ini")
                                            variable_value = variable_value.Split("\n").Where(x => x.Contains("=")).ToDictionary(x => x.Split('=')[0].Trim(' '), x => x.Split('=')[1].Trim(' '))[variable_selector].Replace("\r", "").Replace("\n", "");
                                        else if (Path.GetExtension(variable_source) == ".json")
                                            variable_value = JsonConvert.DeserializeObject<Dictionary<string, object>>(variable_value)[variable_selector] as string;
                                    }
                                }
                            }
                            catch
                            {
                                variable_value = "";
                            }
                        }
                        else
                        {
                            variable_value = VerifyToken(3, "variable value expression");
                        }

                        variables.Add(variable_name, variable_value);
                    }
                    else if (tokens[0] == "print")
                    {
                        var print_expression = VerifyToken(1, "print expression");

                        Console.WriteLine($"PRINT: '{print_expression}'");
                        Debug.WriteLine($"PRINT: '{print_expression}'");
                    }
                }
                catch (BfmeWorkshopScriptSyntaxErrorException syntaxError)
                {
                    throw new BfmeWorkshopScriptSyntaxErrorException($"Syntax error on line {i}. {syntaxError.Message}");
                }
                catch
                {
                    throw;
                }

                string VerifyToken(int index, string expected)
                {
                    if (tokens.Count > index)
                        return tokens[index];
                    else
                        throw new BfmeWorkshopScriptSyntaxErrorException($"Expected '{expected}' missing.");
                }

                void EnforceKeyword(int index, string keyword)
                {
                    if (tokens.Count <= index || tokens[index] != keyword)
                        throw new BfmeWorkshopScriptSyntaxErrorException($"Expected '{keyword}' keyword missing.");
                }
            }

            return (requirements, files_directory);
        }
    }
}
