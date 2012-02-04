using System.Collections.Generic;
using YamlDotNet.RepresentationModel;

namespace Forseti.Configuration
{
	public interface IYamlParser
	{
		IEnumerable<YamlDocument> Parse(string yaml);
	}
}

