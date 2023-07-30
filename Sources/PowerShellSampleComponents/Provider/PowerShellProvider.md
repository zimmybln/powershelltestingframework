# PowerShell Provider

Provider sind eine Möglichkeit der Interaktion bzw. der Spezialisierung von Befehlen.

<span style="color:red">Fragen</span>
- Welche Rolle spielen Schnittstellen (z.B. [IResourceSuplier](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.iresourcesupplier?view=powershellsdk-1.1.) )



###### Leistungsmerkmale

- Verwaltung als hierarchische Strukturen als virtuelle Laufwerke  (Hinzufügen, Entfernen)
- 


#### Konzeptionelle Überlegungen und Beispiele

###### Beispiele

- Konfigurationen im System
- Xml-Datei





Es gibt verschiedene Basisklasse für die Bereitstellung eines Providers. Auch wenn es möglich ist, von jeder der möglichen Basisklassen abzuleiten, sollten für die eigenen Provider lediglich die Klassen ItemCmdletProvider, ContainerCmdletProvider oder NavigationCmdletProvider als Basisklassen verwendet werden.

##### [CmdletProvider](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.provider.cmdletprovider?view=powershellsdk-1.1.0)


##### [DriveCmdletProvider](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.provider.drivecmdletprovider?view=powershellsdk-1.1.0)


##### [ItemCmdletProvider](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.provider.itemcmdletprovider?view=powershellsdk-1.1.0)
Erlaubt das Ermitteln von Informationen über einzelne Elemente auf der Basis einer Pfadangabe. Die dabei unterstützten Befehle sind 

|Befehl|Beschreibung|
|---|---|
|```ClearItem```|asdfasdf|
|```GetItem```||
|```InvokeDefaultAction```||
|```ItemExists```||
|```SetItem```| |
|```Start```||


##### [ContainerCmdletProvider](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.provider.containercmdletprovider?view=powershellsdk-1.1.0)



##### [NavigationCmdletProvider](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.provider.navigationcmdletprovider?view=powershellsdk-1.1.0)
Erlaubt die Verwendung von Rekursiven Befehlen, eingebetteten Containern sowie relativen Pfaden.

|Befehl|Beschreibung|
|---|---|
|```GetParentPath```| |
|```IsItemContainer```||
|```MakePath```||
|```MoveItem```||
|```NormalizeRelativePath```||


#### Anwendungsfälle

##### Alle Bestandteile eines Verzeichnisses auflisten


##### Informationen über ein Element in einem Verzeichnis ermitteln und ausgeben


##### Ein Element von einem Verzeichnis in ein anderes kopieren


##### Ein Element löschen





