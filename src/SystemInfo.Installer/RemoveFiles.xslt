<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:wix="http://schemas.microsoft.com/wix/2006/wi">
    <xsl:output method="xml" indent="yes" />
    <xsl:template match="@*|node()">
        <xsl:copy>
            <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
    </xsl:template>
    <xsl:key name="pdbToRemove" match="wix:Component[contains(wix:File/@Source, '.pdb')]" use="@Id" />
    <xsl:template match="wix:Component[key('pdbToRemove', @Id)]" />
    <xsl:template match="wix:ComponentRef[key('pdbToRemove', @Id)]" />

    <xsl:key name="xmlToRemove" match="wix:Component[contains(wix:File/@Source, 'System.Reactive.xml')]" use="@Id" />
    <xsl:template match="wix:Component[key('xmlToRemove', @Id)]" />
    <xsl:template match="wix:ComponentRef[key('xmlToRemove', @Id)]" />

    <xsl:key name="exeToRemove" match="wix:Component[contains(wix:File/@Source, '.exe')]" use="@Id" />
    <xsl:template match="wix:Component[key('exeToRemove', @Id)]" />
    <xsl:template match="wix:ComponentRef[key('exeToRemove', @Id)]" />
</xsl:stylesheet>