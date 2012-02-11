using System.Collections.Generic;
using System.Yaml;

namespace Forseti.Configuration
{
	public interface IYamlParser
	{
		IEnumerable<YamlNode> Parse(string yaml);
	}
}

