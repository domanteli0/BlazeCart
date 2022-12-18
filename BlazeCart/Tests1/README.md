Add this to `BlazeCart.csproj` to run tests:
``` 
<TargetFrameworks>net6.0;net6.0-android;net6.0-ios;net6.0-maccatalyst</TargetFrameworks>
<OutputType Condition="'$(TargetFramework)' != 'net6.0'">Exe</OutputType>
```
If you want to run `BlazeCart.csproj` after tests, but you have an error, use this, rebuild ant restart VS:
```
<TargetFrameworks>net6.0-android;net6.0-ios;net6.0-maccatalyst</TargetFrameworks>
<OutputType>Exe</OutputType>
```