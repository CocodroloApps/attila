﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

namespace Anonym.Util
{
    using Isometric;

    [System.Serializable]
    // [CreateAssetMenu(fileName = "[MW] New Tile Control", menuName = "Anonym/Magic Wand/Tile Control", order = 1001)]
    public class TileControlWand : MagicWand
    {
        public enum Type
        {
            None,
            Drop_All,
            SpriteRenderer_Color_Override,
            SpriteRenderer_Color_Mix,
            Tile_Control_Copy,
            Tile_Control_Erase,
            Tile_Control_Raise,
            Tile_Control_Lower,
            Tile_Control_Create,
        }

        [SerializeField]
        public Type type = Type.None;

        [SerializeField]
        Texture texture;

#if UNITY_EDITOR
        #region MainFeatures
        public override ParamType[] Params { get {
                if (type == Type.SpriteRenderer_Color_Override || type == Type.SpriteRenderer_Color_Mix)
                    return new ParamType[] { ParamType.Color, ParamType.fWeight, ParamType.Parts };
                else if (type == Type.Tile_Control_Copy)
                    return new ParamType[] { ParamType.IsoTile, ParamType.Parts, ParamType.AutoIsoLight };
                else if (type == Type.Tile_Control_Create)
                    return new ParamType[] { ParamType.Position, ParamType.IsoTile, ParamType.Parts, ParamType.AutoIsoLight, ParamType.IsoBulk };
                return null;
            }
        }

        public override GameObject TargetGameObject(GameObject target)
        {
            IsoTile tile = IsoTile.Find(target);

            if (tile)
            {
                switch(type)
                {
                    case Type.Tile_Control_Raise:
                        tile = tile.FindTop();
                        break;
                    case Type.Tile_Control_Lower:
                        tile = tile.FindTop();
                        break;
                }
            }

            return tile != null ? tile.gameObject : null;
        }

        static bool Drop_AllTile(GameObject target)
        {
            IsoTile tile = IsoTile.Find(target);
            tile.DropToFloor();
            return false;
        }
        static bool RaiseTile(ref GameObject target)
        {
            IsoTile targetTile = IsoTile.Find(target);
            IsoTile topTile = null, leftTile = null;
            if (targetTile != null)
            {
                if ((topTile = targetTile.FindTop()) != null)
                    leftTile = topTile.Extrude(Vector3.up, false);

                if (topTile == targetTile && leftTile != null)
                    target = leftTile.gameObject;
                else
                    target = targetTile.gameObject;
            }
            return targetTile != null;
        }
        static bool LowerTile(ref GameObject target)
        {
            bool bResult = false;
            IsoTile tile = IsoTile.Find(target);
            IsoTile topTile = null;
            if (tile != null)
            {
                if ((topTile = tile.FindTop()) != null)
                    bResult = topTile.Press(Vector3.down);
            }
            if (!target)
                target = topTile.gameObject;
            return bResult;
        }
        static bool Renderer_Color(GameObject target, Type type, Color color, float fBlendWeight, bool includeBody, bool includeAttachment, bool bApplyToTileOverlay = true)
        {
            List<SpriteRenderer> exceptions = new List<SpriteRenderer>();
            List<SpriteRenderer> overlays = new List<SpriteRenderer>();
            var renderers = target.GetComponentsInChildren<SpriteRenderer>();
            var renderersEnumerater = renderers.GetEnumerator();

            if (bApplyToTileOverlay)
            {
                while (renderersEnumerater.MoveNext())
                {
                    var current = renderersEnumerater.Current as SpriteRenderer;
                    var iso2D = current != null ? current.GetComponent<Iso2DObject>() : null;
                    if (iso2D != null)
                    {
                        if (!((includeBody && iso2D.IsSideOfTile) || (includeAttachment && iso2D.IsAttachment)))
                        {
                            exceptions.Add(current);
                            continue;
                        }

                        if (iso2D.IsSideOfTile)
                        {
                            IsoTile tile = iso2D.Tile;
                            overlays.AddRange(tile._attachedList.childList.
                                Where(r => r.AttachedObj.IsTileRCAttachment && r.AttachedObj._Type == Iso2DObject.Type.Overlay).
                                Select(r => r.AttachedObj.sprr).ToArray());
                        }
                    }
                }
                if (overlays.Count > 0)
                    ArrayUtility.AddRange(ref renderers, overlays.ToArray());
                renderersEnumerater = renderers.GetEnumerator();
            }

            Undo.RecordObjects(renderers, "Tile Control Wand: Material Color");
            while (renderersEnumerater.MoveNext())
            {
                var current = renderersEnumerater.Current as SpriteRenderer;
                if (current != null && (!exceptions.Contains(current) || overlays.Contains(current)))
                {
                    var isoLightReciver = current.GetComponent<IsoLightReciver>();
                    if (isoLightReciver != null)
                        isoLightReciver.UpdateBaseColor(CalcColor(isoLightReciver.GetBaseColor, color, type, fBlendWeight));
                    else
                        current.color = CalcColor(current.color, color, type, fBlendWeight);
                }
            }
            return true;
        }

        static bool Tile_Copy(GameObject target, IsoTile refTile, bool bIncludeBody, bool bIncludeAttachments, bool bAutoIsoLight)
        {
            IsoTile tile = IsoTile.Find(target);

            if (tile == null || refTile == null)
                return false;

            tile.Copycat(refTile, bIncludeBody, bIncludeAttachments, true);
            return true;
        }

        public static bool Tile_Create(ref GameObject target, Vector3 position, IsoTile refTile, bool bIncludeBody, bool bIncludeAttachments, bool bAutoIsoLight, IsoTileBulk bulk)
        {
            return IsoTile.Create(ref target, position, refTile, bulk, bIncludeBody, bIncludeAttachments, bAutoIsoLight);
#if UNITY_EDITOR && UNITY_2018_3_OR_NEWER
            return IsoTile.Create(ref target, position, refTile, bulk, bIncludeBody, bIncludeAttachments, bAutoIsoLight, MasterPaletteWindow.bNewPrefabStyle);
#endif
        }

        static bool Tile_Erase(ref GameObject target)
        {
            if (target != null)
            {
                var tile = IsoTile.Find(target);

                if (tile != null)
                    Undo.DestroyObjectImmediate(tile.gameObject);
                else
                    Undo.DestroyObjectImmediate(target);
            }
            target = null;
            return true;
        }

        static Color CalcColor(Color left, Color right, Type type, float fWeight)
        {
            switch(type)
            {
                case Type.SpriteRenderer_Color_Override:
                    left = right;
                    break;
                case Type.SpriteRenderer_Color_Mix:
                    left = new Color(SingleBlend(left.r, right.r, fWeight),
                        SingleBlend(left.g, right.g, fWeight),
                        SingleBlend(left.b, right.b, fWeight), 
                        (1 - fWeight) * left.a + fWeight * right.a);
                    break;
            }
            return left;
        }
        static float SingleBlend(float rgbLeft, float rgbRight, float fWeight)
        {
            fWeight = Mathf.Clamp01(fWeight);
            return Mathf.Sqrt((1 - fWeight) * rgbLeft * rgbLeft + fWeight * rgbRight * rgbRight);
        }
#endregion

        public override Texture[] GetTextures()
        {
            return new Texture[] { texture };
        }

        public override bool MakeUp(ref GameObject target, params object[] values)
        {
            bool bResult = false;
            if (target == null && type != Type.Tile_Control_Create)
                return bResult;

            switch (type)
            {
                case Type.Drop_All:
                    bResult = Drop_AllTile(target);
                    break;
                case Type.SpriteRenderer_Color_Override:
                case Type.SpriteRenderer_Color_Mix:
                    target = TargetGameObject(target);
                    if (target != null)
                        bResult = Renderer_Color(target, type, (Color) values[0], (float) values[1], (bool)values[2], (bool)values[3]);
                    break;
                case Type.Tile_Control_Copy:
                    bResult = Tile_Copy(target, (IsoTile)values[0], (bool)values[1], (bool)values[2], (bool)values[3]);
                    break;
                case Type.Tile_Control_Erase:
                    bResult = Tile_Erase(ref target);
                    break;
                case Type.Tile_Control_Raise:
                    bResult = RaiseTile(ref target);
                    break;
                case Type.Tile_Control_Lower:
                    bResult = LowerTile(ref target);
                    break;
                case Type.Tile_Control_Create:
                    if (!target)
                        bResult = Tile_Create(ref target, (Vector3)values[0], (IsoTile)values[1], (bool)values[2], (bool)values[3], (bool)values[4], (IsoTileBulk)values[5]);
                    else
                        bResult = true;
                    break;
            }
            return bResult;
        }
#endif
        }
    }