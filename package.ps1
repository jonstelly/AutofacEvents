Push-Location
try
{
	cd "Source\Autofac.Events"
	msbuild 
	nuget pack ".\Autofac.Events.csproj"
}
finally
{
	Pop-Location
}


