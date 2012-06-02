namespace Forseti.Files
{
	public interface IPathMatcher
	{
		bool Matches(IFile file);
	}
}

