$Namespace = @{wix="http://schemas.microsoft.com/wix/2006/wi"};

#--------------------------------------------------------
# Search for the file name in the wxs file, determine its ID then remove them
#
#--------------------------------------------------------
function Remove-Node ([System.Xml.XmlNamespaceManager]$nsmgr, [string]$filename)
{
	$XPath = "//wix:Fragment/wix:DirectoryRef/wix:Component/wix:File[@Source='" + $filename + "']/..";
	$removeId = Select-Xml -Xml $xml -Namespace $Namespace -XPath $XPath | Select-Object -ExpandProperty Node | Select Id;
	Write-Host $removeId
	$removeId = $removeId -replace "@{Id=", "";
	$removeId = $removeId -replace "}", "";

	$XPath = "//wix:Fragment/wix:DirectoryRef/wix:Component[@Id=""" + $removeId +"""]";
	$nodeRemove = $xml.SelectSingleNode($XPath, $nsmgr);
	$nodeRemove.ParentNode.RemoveChild($nodeRemove);
	
	$XPath = "//wix:Fragment/wix:ComponentGroup/wix:ComponentRef[@Id=""" + $removeId +"""]";
	$nodeRemove = $xml.SelectSingleNode($XPath, $nsmgr);
	$nodeRemove.ParentNode.RemoveChild($nodeRemove);


}

#--------------------------------------------------------
# Remove ApService files from TailBin.wxs  
# They belong in their own component
#--------------------------------------------------------
function Strip-Service 
{
param (
)   
    
	$Path = ".\TailBin.wxs"
    $xml = [xml](Get-Content $Path);
	[System.Xml.XmlNamespaceManager] $nsmgr = $xml.NameTable;
	$nsmgr.AddNamespace('wix','http://schemas.microsoft.com/wix/2006/wi');	
	$nsmgr.AddNamespace('systemtools','http://schemas.appsecinc.com/wix/SystemToolsExtension');

    Remove-Node $nsmgr '$(var.TailPackageDir)\gilt.tail.service.exe'

	$xml.Save(".\TailBin.wxs");
}

Strip-Service 
