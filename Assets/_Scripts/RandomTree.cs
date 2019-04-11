using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTree : MonoBehaviour
{
    public Tree arbol;
    public MeshFilter mesh;

    public void RandomSeed()
    {
        var _treeController = arbol.data as TreeEditor.TreeData;

        var _root = _treeController.root as TreeEditor.TreeGroupRoot;

        _root.seed = Random.Range(0, 9999999);

        _root.UpdateSeed();

        mesh.sharedMesh.RecalculateBounds();
    }
}
