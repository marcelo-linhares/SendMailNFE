<fo:table-body>
  <fo:table-row>
    <fo:table-cell>
      <fo:block>Item</fo:block>
    </fo:table-cell>
    <fo:table-cell>
      <fo:block>Descricao</fo:block>
    </fo:table-cell>
    <fo:table-cell>
      <fo:block>Preco unitario</fo:block>
    </fo:table-cell>
  </fo:table-row>
  <xsl:for-each select="nfeProc/NFe/infNFe/det/prod">
    <fo:table-row>
      <fo:table-cell>
        <fo:block>
          <xsl:value-of select=".//cProd" />
        </fo:block>
      </fo:table-cell>
      <fo:table-cell>
        <fo:block>
          <xsl:value-of select=".//xProd" />
        </fo:block>
      </fo:table-cell>
      <fo:table-cell>
        <fo:block>
          <xsl:value-of select=".//vUnCom" />
        </fo:block>
      </fo:table-cell>
    </fo:table-row>
  </xsl:for-each>
</fo:table-body>
</fo:table>