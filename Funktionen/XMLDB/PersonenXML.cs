
namespace Funktionen.XMLDB
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class PersonenXML
    {
        private string _sFile = "Person.xml";

        public PersonenXML(string file){
            _sFile = file;
            if (!File.Exists(_sFile))
                _createDB();
        }

        private void _createDB() {
            XmlTextWriter xtw;
            xtw = new XmlTextWriter(_sFile, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("PersonsDetails");
            xtw.WriteEndElement();
            xtw.Close();
        }

        public void WritePersons(object person) {
            var persons = (Model.Person) person;
            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(_sFile, FileMode.Open);
            xd.Load(lfile);
            XmlElement cl = xd.CreateElement("Persons");
            cl.SetAttribute("Name", persons.Name);
            cl.SetAttribute("Street", persons.Street);
            cl.SetAttribute("PostalCode", persons.PostalCode.ToString());
            cl.SetAttribute("City", persons.City);
            xd.DocumentElement.AppendChild(cl);
            lfile.Close();
            xd.Save(_sFile);
        }

        public object ReadAllPerson() {
            var result = new List<Model.Person>();
            XmlDocument xdoc = new XmlDocument();
            FileStream rfile = new FileStream(_sFile, FileMode.Open);
            xdoc.Load(rfile);
            XmlNodeList list = xdoc.GetElementsByTagName("Persons");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)xdoc.GetElementsByTagName("Persons")[i];
                result.Add(new Model.Person(cl.GetAttribute("Name"), cl.GetAttribute("Street"), Convert.ToInt32(cl.GetAttribute("PostalCode")), cl.GetAttribute("City")));
            }
            rfile.Close();
            return (List<Model.Person>)result;
        }

        public void UpdatePerson(object person){
            var persons = (Model.Person)person;
            XmlDocument xdoc = new XmlDocument();
            FileStream up = new FileStream(_sFile, FileMode.Open);
            xdoc.Load(up);
            XmlNodeList list = xdoc.GetElementsByTagName("Persons");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cu = (XmlElement)xdoc.GetElementsByTagName("Persons")[i];
                if (cu.GetAttribute("Name") == persons.Name)
                {
                    cu.SetAttribute("Street", persons.Street);
                    cu.SetAttribute("PostalCode", persons.PostalCode.ToString());
                    cu.SetAttribute("City", persons.City);
                    break;
                }
            }
            up.Close();
            xdoc.Save(_sFile);
        }

        public void RemovePerson(object person) {
            var persons = (Model.Person)person;
            FileStream rfile = new FileStream(_sFile, FileMode.Open);
            XmlDocument tdoc = new XmlDocument();
            tdoc.Load(rfile);
            XmlNodeList list = tdoc.GetElementsByTagName("Persons");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement cl = (XmlElement)tdoc.GetElementsByTagName("Persons")[i];
                if (cl.GetAttribute("Name") == persons.Name)
                {
                    tdoc.DocumentElement.RemoveChild(cl);
                }
            }
            rfile.Close();
            tdoc.Save(_sFile);
        }

        public void CreatePersonDB()
        {
            _createDB();
        }

        public override string ToString()
        {
            return this._sFile;
        }
    }
}
