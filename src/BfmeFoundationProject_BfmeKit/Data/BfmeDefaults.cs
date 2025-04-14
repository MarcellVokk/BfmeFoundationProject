namespace BfmeFoundationProject.BfmeKit.Data
{
    public static class BfmeDefaults
    {
        public static Dictionary<int, string> DefaultGameRegistryKeys = new()
        {
            { 0, @"Electronic Arts\EA Games\The Battle for Middle-earth" },
            { 1, @"Electronic Arts\Electronic Arts\The Battle for Middle-earth II" },
            { 2, @"Electronic Arts\Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" }
        };

        public static Dictionary<int, string> DeprecatedGameRegistryKeys = new()
        {
            { 0, @"EA GAMES\The Battle for Middle-earth" },
            { 1, @"Electronic Arts\The Battle for Middle-earth II" },
            { 2, @"Electronic Arts\The Lord of the Rings, The Rise of the Witch-king" }
        };

        public static Dictionary<int, string> DefaultUserDataLeafNames = new()
        {
            { 0, "My Battle for Middle-earth Files" },
            { 1, "My Battle for Middle-earth II Files" },
            { 2, "My Rise of the Witch-king Files" }
        };

        public static Dictionary<int, string> DefaultGameExecutableNames = new()
        {
            { 0, "lotrbfme.exe" },
            { 1, "lotrbfme2.exe" },
            { 2, "lotrbfme2ep1.exe" }
        };

        public static string DefaultOptions = @$"AllHealthBars = yes
AlternateMouseSetup = no
AmbientVolume = 50.000000
AudioLOD = High
Brightness = 50
FixedStaticGameLOD = UltraHigh
FlashTutorial = 0
HasGotOnline = yes
HasSeenLogoMovies = yes
HeatEffects = yes
IdealStaticGameLOD = UltraHigh
IsThreadedLoad = yes
MovieVolume = 70.000000
MusicVolume = 70.000000
Resolution = 1920 1080
SFXVolume = 70.000000
ScrollFactor = 50
StaticGameLOD = UltraHigh
TimesInGame = 1
UnitDecals = yes
UseEAX3 = yes
VoiceVolume = 70.000000";
    }
}
