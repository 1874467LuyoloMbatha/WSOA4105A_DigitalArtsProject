using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace StudYolo.Blendshapes
{
    public class CharacterCustomisation : Singleton<CharacterCustomisation>
    {

        public SkinnedMeshRenderer target;
        public string suffixMax = "Max", suffixMin = "Min";

        private CharacterCustomisation() { }

        private SkinnedMeshRenderer skmr;
        private Mesh mesh;

        private Dictionary<string, BlendShapeWrapper> blendShapeDatabase = new Dictionary<string, BlendShapeWrapper>();

        private void Start()
        {
            Initialize();
        }


        #region Public Functions

        public void ChangeBlendshapeValue(string blendshapeName, float value)
        {
            if (!blendShapeDatabase.ContainsKey(blendshapeName)) { Debug.LogError("Blendshape " + blendshapeName + " does not exist!"); return; }

            value = Mathf.Clamp(value, -100, 100);

            BlendShapeWrapper blendshape = blendShapeDatabase[blendshapeName];

            if (value >= 0)
            {
                if (blendshape.positiveIndex == -1) return;
                skmr.SetBlendShapeWeight(blendshape.positiveIndex, value);
                if (blendshape.negativeIndex == -1) return;
                skmr.SetBlendShapeWeight(blendshape.negativeIndex, 0);
            }

            else
            {
                if (blendshape.negativeIndex == -1) return;
                skmr.SetBlendShapeWeight(blendshape.negativeIndex, -value);
                if (blendshape.positiveIndex == -1) return;
                skmr.SetBlendShapeWeight(blendshape.positiveIndex, 0);
            }

        }

        #endregion

        #region Private Functions

        public void Initialize()
        {
            //if (target == null) print("DSKDJLASJDIWJLSJDLSKJ");

            skmr = target;
            mesh = skmr.sharedMesh;

            ParseBlendShapesToDictionary();

            //print("I HAVE BEEN PARSED! I HAVE " + blendShapeDatabase.Count + " entries!");
        }

        private SkinnedMeshRenderer GetSkinnedMeshRenderer()
        {
            SkinnedMeshRenderer _skmr = target.GetComponentInChildren<SkinnedMeshRenderer>();

            return _skmr;
        }

        private void ParseBlendShapesToDictionary()
        {
            //Get all blendshape names
            List<string> blendshapeNames = Enumerable.Range(0, mesh.blendShapeCount).Select(x => mesh.GetBlendShapeName(x)).ToList();
            int index = 0;

            for (int i = 0; blendshapeNames.Count > 0;)
            {
                string altSuffix, noSuffix;
                //Removes the max and min suffixes 
                noSuffix = blendshapeNames[index].TrimEnd(suffixMax.ToCharArray()).TrimEnd(suffixMin.ToCharArray()).Trim();
                //Debug.Log(index);

                string positiveName = string.Empty, negativeName = string.Empty;
                bool exists = false;

                int postiveIndex = -1, negativeIndex = -1;

                //If Suffix is Postive
                if (blendshapeNames[index].EndsWith(suffixMax))
                {
                    altSuffix = noSuffix + " " + suffixMin;

                    positiveName = blendshapeNames[index];
                    negativeName = altSuffix;

                    if (blendshapeNames.Contains(altSuffix)) exists = true;

                    postiveIndex = mesh.GetBlendShapeIndex(positiveName);

                    if (exists)
                        negativeIndex = mesh.GetBlendShapeIndex(altSuffix);
                }

                //If Suffix is Negative
                else if (blendshapeNames[index].EndsWith(suffixMin))
                {
                    altSuffix = noSuffix + " " + suffixMax;

                    negativeName = blendshapeNames[index];
                    positiveName = altSuffix;

                    if (blendshapeNames.Contains(altSuffix)) exists = true;

                    negativeIndex = mesh.GetBlendShapeIndex(negativeName);

                    if (exists)
                        postiveIndex = mesh.GetBlendShapeIndex(altSuffix);
                }

                //Doesn't have a suffix
                else
                {
                    postiveIndex = mesh.GetBlendShapeIndex(blendshapeNames[index]);
                    positiveName = noSuffix;    //This is here so it will remove it (for condition) so its not infinite loop
                    //print(postiveIndex + " of " + noSuffix);                    
                }


                if (blendShapeDatabase.ContainsKey(noSuffix))
                    Debug.LogError(noSuffix + " already exists within the Database!");

                blendShapeDatabase.Add(noSuffix, new BlendShapeWrapper(postiveIndex, negativeIndex));


                //Remove Selected Indexes From the List
                if (positiveName != string.Empty) blendshapeNames.Remove(positiveName);
                if (negativeName != string.Empty) blendshapeNames.Remove(negativeName);

                if (index >= blendshapeNames.Count)
                    return;
                else
                    index++;

            }//End of Loop
        }//End of Class

        #endregion

        //Get all registered Blendshapes names without suffixes (The Dictionary Keys)
        public string[] GetBlendShapeNames()
        {
            return blendShapeDatabase.Keys.ToArray();
        }

        public int GetNumberOfEntries()
        {
            return blendShapeDatabase.Count;
        }

        public BlendShapeWrapper GetBlendshape(string name)
        {
            return blendShapeDatabase[name];
        }

        //Use for editor to check if the Target has been changed so needs to update accordingly
        public bool DoesTargetMatchSkmr()
        {
            return (target == skmr) ? true : false;
        }

        public void ClearDatabase()
        {
            blendShapeDatabase.Clear();
        }
    }
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/