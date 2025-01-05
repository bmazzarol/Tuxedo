using System.Diagnostics;
using Examples;

Console.WriteLine("Welcome to Tuxedo!");

// only positive integers work
#pragma warning disable TUX003 // this is required otherwise it will not compile
_ = (PositiveInt)(-1); // this throws an ArgumentOutOfRange exception at runtime and will not compile if TUX003 is enabled
#pragma warning restore TUX003
// this is fine
var v2 = (PositiveInt)1;
// all default creation paths are also disallowed
#pragma warning disable TUX001 // this is required otherwise it will not compile
_ = default(PositiveInt);
#pragma warning restore TUX001
#pragma warning disable TUX002 // this is required otherwise it will not compile
_ = new PositiveInt();
#pragma warning restore TUX002

// we can also create more advanced types like this non-empty array
var nea = NonEmptyArray<PositiveInt>.Parse([v2]);

// this type has a positive length and can always access head
int length = nea.Length; // implicit conversion back to raw type
int head = nea.Head;

Debug.Assert(head == length);

// if fail fast is not your vibe, then use the TryParse methods
if (NonEmptyArray<PositiveInt>.TryParse([v2], out var v5, out _))
{
    Debug.Assert(nea.Equals(v5));
}
