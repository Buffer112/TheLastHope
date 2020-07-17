/// Limerence Games
/// The Last Hope
/// Curator: Dmitri Kuzmin
/// to be commented

using UnityEngine;
using System.Collections;

namespace TheLastHope.Workers
{
    public class CursorSetter : MonoBehaviour
    {
        [SerializeField] Texture2D cursorTexture;
        [SerializeField] CursorMode cursorMode = CursorMode.Auto;
        [SerializeField] Vector2 hotSpot = Vector2.zero;

        public Texture2D CursorTexture { get { return cursorTexture; } set { cursorTexture = value; } }
        public CursorMode CursorMode { get { return cursorMode; } set { cursorMode = value; } }
        public Vector2 HotSpot { get { return hotSpot; } set { hotSpot = value; } }

        void OnMouseEnter()
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }

        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
