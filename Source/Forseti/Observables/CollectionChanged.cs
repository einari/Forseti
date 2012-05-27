using System.Collections.Generic;

namespace Forseti.Observables
{
	public delegate void CollectionChanged(CollectionAction action, IEnumerable<object> items);
}

