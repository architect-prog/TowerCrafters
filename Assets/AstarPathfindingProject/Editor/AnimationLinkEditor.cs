using UnityEngine;
using UnityEditor;

namespace Pathfinding {
	[CustomEditor(typeof(AnimationLink))]
	public class AnimationLinkEditor : Editor {
		public override void OnInspectorGUI () {
			DrawDefaultInspector();

			var script = target as AnimationLink;

			EditorGUI.BeginDisabledGroup(script.EndTransform == null);
			if (GUILayout.Button("Autoposition Endpoint")) {
				var buffer = Pathfinding.Util.ListPool<Vector3>.Claim();
				Vector3 endpos;
				script.CalculateOffsets(buffer, out endpos);
				script.EndTransform.position = endpos;
				Pathfinding.Util.ListPool<Vector3>.Release(buffer);
			}
			EditorGUI.EndDisabledGroup();
		}
	}
}
