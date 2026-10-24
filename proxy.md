# Proxy Configuration

The application reads its proxy settings from a JSON file named **`proxy-configuration.json`**, which must be placed in the **application directory** (next to the app executable).

- **When it is read:** the file is loaded **once, at application startup**. Changes made while the app is running are **not** picked up — restart the application to apply new settings.
- **If the file is missing or fields are omitted**, the documented default values below apply.

## File structure

```json
{
  "ProxyType": "None",
  "Url": "",
  "Bypasses": [],
  "BypassLocal": true,
  "UseAuthorization": false,
  "Username": "",
  "Password": "",
  "Domain": null
}
```

## Settings reference


| Property           | Type          | Default      | Description                                                                                                            |
| ------------------ | ------------- | ------------ | ---------------------------------------------------------------------------------------------------------------------- |
| `ProxyType`        | string        | `"None"`     | Proxy protocol to use. See the table below for accepted values.                                                        |
| `Url`              | string        | `""` (empty) | Address of the proxy server, including the port. **Required** for every `proxyType` except `None` and `System`.        |
| `Bypasses`         | string\[\]    | `[]` (empty) | List of addresses the proxy should **not** be used for. Supports wildcards, e.g. `"*.example.com"` or `"192.168.1.*"`. |
| `BypassLocal`      | bool          | `true`       | When `true`, local (intranet/LAN) addresses bypass the proxy and are accessed directly.                                |
| `UseAuthorization` | bool          | `false`      | When `true`, the app authenticates on the proxy using the credentials below.                                           |
| `Username`         | string        | `""` (empty) | Proxy username. Used only when `useAuthorization` is `true`.                                                           |
| `Password`         | string        | `""` (empty) | Proxy password. Used only when `useAuthorization` is `true`. Stored in plain text — restrict access to the file.       |
| `Domain`           | string | null | `null`       | Optional domain (mainly for NTLM/Windows credentials). Can be omitted entirely.                                        |


## Proxy types

The value of `proxyType` is a **case-sensitive string** matching one of the enum names below (numbers are not accepted):


| Value          | Meaning                                                                                                                                        |
| -------------- | ---------------------------------------------------------------------------------------------------------------------------------------------- |
| `None`         | No proxy. All connections go directly.                                                                                                         |
| `System`       | Use the operating system's proxy settings. `url` and other fields are ignored.                                                                 |
| `Http`         | HTTP proxy. Set `url` to e.g. `"http://proxy.example.com:8080"`.                                                                               |
| `Https`        | HTTPS proxy. Set `url` to e.g. `"https://proxy.example.com:8443"`.                                                                             |
| `Socks4`       | SOCKS4 proxy. The target hostname is resolved locally, so only IP-address targets are supported. Set `url` to e.g. `"socks4://10.0.0.1:1080"`. |
| `Socks4Domain` | SOCKS4a. The target hostname is sent to the proxy, which resolves it — useful when local DNS is unavailable.                                   |
| `Socks5`       | SOCKS5 with local hostname resolution.                                                                                                         |
| `Socks5Domain` | SOCKS5 with remote hostname resolution (the proxy performs the DNS lookup).                                                                    |


## Examples

### No proxy

```json
{
  "ProxyType": "None"
}
```

### Use the system proxy

```json
{
  "ProxyType": "System",
  "BypassLocal": true
}
```

### HTTP proxy with authentication and bypass list

```json
{
  "ProxyType": "Http",
  "Url": "http://proxy.example.com:8080",
  "Bypasses": ["*.internal.example.com", "192.168.1.*"],
  "BypassLocal": true,
  "UseAuthorization": true,
  "Username": "proxyuser",
  "Password": "secret",
  "Domain": null
}
```

### SOCKS5 proxy (proxy-side DNS)

```json
{
  "ProxyType": "Socks5Domain",
  "Url": "socks5://10.0.0.1:1080",
  "UseAuthorization": true,
  "Username": "proxyuser",
  "Password": "secret"
}
```

## Notes

- The file must be named exactly `proxy-configuration.json` and located in the application directory.
- Omitted fields fall back to their defaults shown above; the minimal valid file is `{ "ProxyType": "None" }`.
- Unknown or unsupported values of `ProxyType` will cause the configuration to fail to load — use only the listed names.
- Proxy credentials are stored in plain text. Make sure the file has the most restrictive access permissions possible for your environment.