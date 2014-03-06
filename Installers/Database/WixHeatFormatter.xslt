<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
            xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
            xmlns:msxsl="urn:schemas-microsoft-com:xslt"
            exclude-result-prefixes="msxsl"
            xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">

    <xsl:output method="xml" indent="yes" />
	
	<xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>

    <!-- output the Variables.wxi include statement-->
	<xsl:template match="wix:Fragment[1]">        		
		<xsl:text disable-output-escaping="yes">&lt;?include Variables.wxi?&gt;&#xa;</xsl:text>		
		<xsl:copy>         			
			<xsl:apply-templates select="@*|node()"/>
        </xsl:copy>        
    </xsl:template>	

	<!-- add the ProcessorArchitecture attribute -->
    <xsl:template match="wix:File">	
		<xsl:copy>            	
			<xsl:attribute name="ProcessorArchitecture">$(var.Platform)</xsl:attribute>							
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>	
    </xsl:template>		
	<!-- Set the MainAppExe -->
    <xsl:template match="wix:File[@Source = '$(var.GiltlyDatabaseSchemaPackageDir)\Schema.sql']">	
		<xsl:copy>            									
			<xsl:attribute name="Id">MainAppExe</xsl:attribute>	
			<xsl:attribute name="KeyPath">yes</xsl:attribute>	
			<xsl:attribute name="ProcessorArchitecture">$(var.Platform)</xsl:attribute>
			<xsl:attribute name="Source">$(var.GiltlyDatabaseSchemaPackageDir)\Schema.sql</xsl:attribute>
		</xsl:copy>	
    </xsl:template>		
	
	<!-- add the win64 component attribute -->	
	<xsl:template match="wix:Component">
        <xsl:copy>
            <xsl:attribute name="Win64">$(var.Win64Flag)</xsl:attribute>
			<xsl:apply-templates select="@*|node()"/>
        </xsl:copy>
    </xsl:template>
	
</xsl:stylesheet>