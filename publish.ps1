Push-Location
try
{
	cd "Source\Autofac.Events"
	nuget push ".\Autofac.Events.3.5.2.1.nupkg"
}
finally
{
	Pop-Location
}


