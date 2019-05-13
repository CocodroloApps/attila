using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Anonym.Util
{
    using Isometric;

    [CreateAssetMenu(fileName = "New Tile Wand", menuName = "Anonym/Magic Wand/Tile", order = 998)]
    public class TileWand : MagicWand
    {
        [SerializeField]
        public GameObject Prefab;

        [SerializeField]
        public TmpTexture2D textureForGUI = new TmpTexture2D();

        [SerializeField, HideInInspector]
        private Sprite[] sprites = null;

        [SerializeField, HideInInspector]
        private Color[] colors = null;

        public IsoTile Tile { get { return Prefab == null ? null : Prefab.GetComponent<IsoTile>(); } }

#if UNITY_EDITOR

        public override ParamType[] Params
        {
            get
            {
                return new ParamType[] {ParamType.New, ParamType.Position, ParamType.Parts, ParamType.AutoIsoLight , ParamType.IsoBulk};
            }
        }

        public SpriteRenderer[] GetSpriteRenderers()
        {
            if (Prefab != null)
                return Prefab.GetComponentsInChildren<SpriteRenderer>();
            return null;
        }

        public override Texture[] GetTextures()
        {
            var sprr = GetSpriteRenderers();
            if (sprr != null)
                return sprr.Select(s => s.sprite.texture).ToArray();
            return null;
        }

        public override Color[] GetColors()
        {
            var sprr = GetSpriteRenderers();
            if (sprr != null)
                return sprr.Select(s => s.color).ToArray();
            return null;
        }

        public void UpdateIcon()
        {
            textureForGUI.bCorrupted = true;
            MakeIcon(this);
        }

        protected override void OnCustomGUI(Rect rect)
        {
            if (textureForGUI.bCorrupted == true)
                MakeIcon(this);

            textureForGUI.DrawRect(rect);
            return;
        }

        public override GameObject TargetGameObject(GameObject target)
        {
            IsoTile tile = IsoTile.Find(target);
            return tile != null ? tile.gameObject : null;
        }

        public override bool MakeUp(ref GameObject target, params object[] values)
        {
            bool bAutoCreation = (bool)values[0];
            Vector3 vAt = (Vector3)values[1];
            bool bBody = (bool)values[2];
            bool bAttachments = (bool)values[3];
            bool bAutoIsoLight = (bool)values[4];
            IsoTileBulk refBulk = (IsoTileBulk)values[5];
            IsoTile refTile = Tile;
            IsoTile TargetTile = IsoTile.Find(target);
            bool bPrefabConnected_TargetTile = target != null && target.IsPrefabConnected();

#if UNITY_EDITOR && UNITY_2018_3_OR_NEWER
            if (MasterPaletteWindow.bNewPrefabStyle)
            {
                bool bPrefabConnected_RefTile = refTile != null && refTile.IsPrefabConnected();
                if (bPrefabConnected_RefTile)// && bPrefabConnected_TargetTile)
                {
                    if (bPrefabConnected_TargetTile)
                    {
                        var one = PrefabUtility.GetCorrespondingObjectFromSource(target);
                        if (one.transform.IsChildOf(refTile.transform))
                            return false;
                    }                    
                }
            }
#endif

            bool bTileAtPosition = false;

            if (TargetTile)
            {
                // 타겟 타일이 있을 경우 그것이 파라미터 좌표에 있는 타일이 맞는지 대조하는 것 같다.
                // 신규 목표는 bAutoStack일 경우 타일이 포지션에 있다고 판정하고 싶음
                // 내포되어 있던 문제는 기존 코드가 coordinate와 position을 혼용해서 사용하고 있었다는 것
                Vector3 vPositionParamCoordinates = vAt;
                if (TargetTile.Bulk)
                    vPositionParamCoordinates -= TargetTile.Bulk.transform.position;
                vPositionParamCoordinates = TargetTile.Bulk.coordinates.PositionToCoordinates(vPositionParamCoordinates, !TargetTile.coordinates.bSnapFree);
                bTileAtPosition = GridCoordinates.IsSameWithTolerance(vPositionParamCoordinates, TargetTile.coordinates._xyz);
            }

            // 타일을 지워야 할 경우. 대상이 프리팹일 때, 또는 대상이 프리팹이 아니지만 프리팹으로 배치할 때
            if (TargetTile != null)
            {
#if UNITY_EDITOR && UNITY_2018_3_OR_NEWER
                if (bPrefabConnected_TargetTile || MasterPaletteWindow.bNewPrefabStyle)
#else
                if (bPrefabConnected_TargetTile)
#endif
                {
                    if (bTileAtPosition)
                        Undo.DestroyObjectImmediate(TargetTile.gameObject);
                }

            }

            if (!bTileAtPosition || target == null)
            {
                if (!bAutoCreation)
                    return false;

                if (TileControlWand.Tile_Create(ref target, vAt, refTile, bBody, bAttachments, bAutoIsoLight, refBulk))
                {
                    TargetTile = IsoTile.Find(target);
                    bPrefabConnected_TargetTile = target.IsPrefabConnected();
                }
            }

            if (refTile == null || TargetTile == null)
                return false;

            // Undo.Record() code is aleady in Copycat()
            if (!bPrefabConnected_TargetTile)
                TargetTile.Copycat(refTile, bBody, bAttachments, true);

            target = TargetTile.gameObject;
            return true;
        }

        public static void MakeIcon(TileWand tileWand)
        {
            if (tileWand != null && tileWand.textureForGUI != null)
            {
                if (tileWand.Prefab == null)
                {
                    DestroyImmediate(tileWand.textureForGUI.texture, true);
                    tileWand.textureForGUI.texture = null;
                    tileWand.textureForGUI.bCorrupted = false;
                    AssetDatabase.SaveAssets();
                    return;
                }
                tileWand.sprites = tileWand.GetSpriteRenderers().Select(s => s.sprite).ToArray();
                tileWand.colors = tileWand.GetColors().ToArray();

                Camera camera = FindObjectOfType<Camera>();
                Texture2D _tex = camera != null 
                    ? tileWand.textureForGUI.MakeRenderImage(tileWand.Prefab.GetComponent<IsoTile>().gameObject, camera, Color.clear) 
                    : tileWand.textureForGUI.MakeIcon(ref tileWand.sprites, ref tileWand.colors);
                
                if (_tex == null)
                {
                    tileWand.textureForGUI.bCorrupted = false;
                    return ;
                }

                var childs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(tileWand));
                var expiredList = childs.Where(c => c != tileWand).ToArray();
                for (int i = 0; i < expiredList.Length; ++i)
                {
                    DestroyImmediate(expiredList[i], true);
                }

                AssetDatabase.AddObjectToAsset(_tex, tileWand);
                tileWand.textureForGUI.texture = _tex;

                tileWand.textureForGUI.bCorrupted = false;

                EditorUtility.SetDirty(tileWand.textureForGUI.texture);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return;
            }
            return;
        }
        public static TileWand CreateAsset(string path, IsoTile tile, bool bOverride = false)
        {
            TileWand newTileWand = ScriptableObject.CreateInstance<TileWand>();

            string _path = bOverride ? path : AssetDatabase.GenerateUniqueAssetPath(path + ".asset");
            AssetDatabase.CreateAsset(newTileWand, _path);

            GameObject tileGameObject = tile.gameObject;
            if (!PrefabHelper.IsPrefab(tileGameObject))
            {
                path = System.IO.Path.ChangeExtension(path, ".prefab");
                _path = bOverride ? path : AssetDatabase.GenerateUniqueAssetPath(path);
                tileGameObject = PrefabHelper.CreatePrefab(_path, tileGameObject);
            }

            // Revert All Lightings
            tile = IsoTile.Find(tileGameObject);
            tile.LightRecivers_RevertAll();
            tile.LightRecivers_RemoveAll(false);

            IsometricSortingOrder iso = tile.GetComponent<IsometricSortingOrder>();
            iso.Clear_Backup(false);

            newTileWand.Prefab = tileGameObject;

            MakeIcon(newTileWand);

            if (newTileWand != null)
            {
                // AssetDatabase.AddObjectToAsset(tilePrefab, newTileWand);            
                EditorUtility.SetDirty(newTileWand);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            if (newTileWand == null)
                newTileWand = AssetDatabase.LoadAssetAtPath<TileWand>(_path);

            return newTileWand;
        }
#endif
            }
        }