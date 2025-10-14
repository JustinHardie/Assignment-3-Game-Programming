#if UNITY_EDITOR
using UnityEditor;
// Forces Unity to build Assembly-CSharp-Editor.dll
public class DummyEditor : Editor {}
#endif
