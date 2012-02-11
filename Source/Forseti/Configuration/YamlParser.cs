using System.Collections.Generic;
using System.IO;
using System.Yaml;

namespace Forseti.Configuration
{
	public class YamlParser : IYamlParser
	{
		public IEnumerable<YamlNode> Parse (string yaml)
		{
			var nodes = YamlNode.FromYaml(yaml);
			return nodes;
		}
		
	}
}

