using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace Forseti.Configuration
{
	public class YamlParser : IYamlParser
	{
		public IEnumerable<YamlDocument> Parse (string yaml)
		{
			using (var stream = new StringReader(yaml)) 
			{
				var yamlStream = new YamlStream();
				yamlStream.Load (stream);
				return yamlStream.Documents;
			}
		}
		
	}
}

