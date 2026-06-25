$packages = @(
	@{id="MySql.Data"; version="9.0.0"},
	@{id="Newtonsoft.Json"; version="13.0.3"},
	@{id="BouncyCastle.Cryptography"; version="2.4.0"},
	@{id="DG.AdvancedDataGridView"; version="1.2.30115.18"},
	@{id="Google.Protobuf"; version="3.27.2"},
	@{id="K4os.Compression.LZ4"; version="1.3.8"},
	@{id="K4os.Compression.LZ4.Streams"; version="1.3.8"},
	@{id="K4os.Hash.xxHash"; version="1.0.8"},
	@{id="Microsoft.Bcl.AsyncInterfaces"; version="8.0.0"},
	@{id="System.Buffers"; version="4.5.1"},
	@{id="System.Configuration.ConfigurationManager"; version="8.0.0"},
	@{id="System.Diagnostics.DiagnosticSource"; version="8.0.1"},
	@{id="System.IO.Pipelines"; version="8.0.0"},
	@{id="System.Memory"; version="4.5.5"},
	@{id="System.Numerics.Vectors"; version="4.5.0"},
	@{id="System.Runtime.CompilerServices.Unsafe"; version="6.0.0"},
	@{id="System.Threading.Tasks.Extensions"; version="4.5.4"},
	@{id="ZstdSharp.Port"; version="0.8.1"}
)

$packagesDir = "..\packages"

if (-not (Test-Path $packagesDir)) {
	New-Item -ItemType Directory -Path $packagesDir | Out-Null
}

foreach ($pkg in $packages) {
	$nupkgName = "$($pkg.id).$($pkg.version).nupkg"
	$pkgPath = Join-Path $packagesDir $nupkgName
	$nupkgUrl = "https://www.nuget.org/api/v2/package/$($pkg.id)/$($pkg.version)"

	if (-not (Test-Path $pkgPath)) {
		Write-Host "Downloading $nupkgName..."
		try {
			Invoke-WebRequest -Uri $nupkgUrl -OutFile $pkgPath -ErrorAction Stop
			Write-Host "  Downloaded: $nupkgName"
		} catch {
			Write-Host "  Failed to download $nupkgName : $_"
		}
	} else {
		Write-Host "Already exists: $nupkgName"
	}
}

Write-Host "Package download complete!"
Get-ChildItem $packagesDir | Select-Object Name
