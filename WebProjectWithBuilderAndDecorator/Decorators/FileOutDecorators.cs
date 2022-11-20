using System.Xml;
using WebProjectWithBuilderAndDecorator.Models;

class XmlOutDecorator : FileDecorator
{
    public XmlOutDecorator(IFileImprovement file) : base(file) { }

    public override FileItem FileImprovement()
    {
        FileItem improve = base.FileImprovement();

        XmlWriter xmlWriter = XmlWriter.Create("E://result.xml");
        xmlWriter.WriteStartDocument();

        xmlWriter.WriteStartElement("Results");

        xmlWriter.WriteStartElement("Result");
        xmlWriter.WriteString(improve.ExpressionParsing().ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("MD5_Result");
        xmlWriter.WriteString(improve.EncryptedExpression);
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();
        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
        return improve;
    }
}