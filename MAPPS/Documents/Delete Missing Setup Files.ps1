Add-PSSnapin Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue

$deleteForReal = $false
 
Function Run-SQLScript($SQLServer, $SQLDatabase, $SQLQuery) {
    $ConnectionString = "Server =" + $SQLServer + "; Database =" + $SQLDatabase + "; Integrated Security = True"
    $Connection = new-object system.data.SqlClient.SQLConnection($ConnectionString)
    $Command = new-object system.data.sqlclient.sqlcommand($SQLQuery,$Connection)
    $Connection.Open()
    $Adapter = New-Object System.Data.sqlclient.sqlDataAdapter $Command
    $Dataset = New-Object System.Data.DataSet
    $Adapter.Fill($Dataset) 
    $Connection.Close()
    $Dataset.Tables[0]
}


$webApps = Get-SPWebApplication
foreach ($wa in $webApps | ? {$_.Name -notlike "*mysite*"}) {
    $contentDatabases = Get-SPContentDatabase -WebApplication $wa
    foreach ($contentDB in $contentDatabases){
        $errors = Test-SpContentDatabase -Name $contentDB.Name -WebApplication $wa -ServerInstance $contentDB.Server -ShowLocation:$true -ExtendedCheck:$false
        foreach ($e in $errors) {
            if ($e.Category -eq "MissingSetupFile"){
                $startOfFile = $e.Message.IndexOf("[") + 1
                $endOfFile = $e.Message.IndexOf("]") -6
                $file = $e.Message.Substring($startOfFile, $endOfFile)
                Write-Host ------ File =  $file
                Write-Host ------ CDB = $contentDB.Name
                Write-Host ------ Server = $contentDB.Server

                # call the function passing all three values
    
                $Server = $contentDB.Server
                $Database = $contentDB.Name
                $SetupFile = $file
 
                #Query SQL Server content Database to get information about the MissingFiles
                $Query = "SELECT * from AllDocs where SetupPath like '"+$SetupFile+"'"
                $QueryResults = @(Run-SQLScript -SQLServer $Server -SQLDatabase $Database -SQLQuery $Query | select Id, SiteId, WebId)

                #Iterate through results
                foreach ($Result in $QueryResults) {
                    if($Result.id -ne $Null) {
                        $Site = Get-SPSite -Limit all | where { $_.Id -eq $Result.SiteId }
                        $Web = $Site | Get-SPWeb -Limit all | where { $_.Id -eq $Result.WebId }
 
                        #Get the URL of the file which is referring the feature
                        $File = $web.GetFile([Guid]$Result.Id)
                        if($deleteForReal){
                            write-host "$($Web.URL)/$($File.Url)" -foregroundcolor green
                            $File.delete()
                        } else{
                            write-host "$($Web.URL)/$($File.Url) would have been deleted" -foregroundcolor red
                        }
                    }
                }
            }
        }
    }
}