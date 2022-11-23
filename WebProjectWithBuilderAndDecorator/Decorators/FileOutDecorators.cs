using Newtonsoft.Json;
using System.IO;
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

public class JsonObject
{
    [JsonProperty("expressions")]
    public FileItem _item;
}

class JsonOutDecorator : FileDecorator
{
    public JsonOutDecorator(IFileImprovement file) : base(file) { }

    public override FileItem FileImprovement()
    {
        FileItem improve = base.FileImprovement();

        var json = JsonConvert.SerializeObject(new
        {
            exp = improve.ExpressionParsing(),
            md5exp = improve.EncryptedExpression
        },
        Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(@"E://JSONResult.json", json);
        return improve;
    }
    
}