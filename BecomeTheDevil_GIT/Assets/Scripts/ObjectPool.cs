using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 오브젝트 풀은 재사용하려는 오브젝트들을 담아 놓는 풀임. ex) 총알
/// </summary>
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// An array of prefabs used to create an object in the gameworld
    /// 게임에서 오브젝트들을 만드는데 사용되는 프리팹 배열.
    /// </summary>
    [SerializeField]
    private GameObject[] objectPrefabs;

    /// <summary>
    /// A list of objects in the pool
    /// 풀 안에 있는 오브젝트들을 List 형태의 STL로 관리.
    /// List의 장점 : 중간에 있는 값들을 제거하거나 삽입할 때 수행 시간이 빠름.
    /// </summary>
    private List<GameObject> pooledObjects = new List<GameObject>();
    
    /// <returns>A GameObject of specific type</returns>
    /// type과 같은 이름을 가진 GameObject를 반환해줌
    public GameObject GetObject(string type)        
    {
        // 풀에서 object를 찾음.
        foreach (GameObject go in pooledObjects)
        {
            // 풀 안에 우리가 원하는 얘가 있으면 재사용.
            if (go.name == type && !go.activeInHierarchy)
            {
                /*gameObject.activeInHierarchy    // 하이어라키상에서 활성상태 여부. activeSelf가 활성상태여도 상위 부모 GameObject가 비활성상태라면 자식인 자신도 비활성상태이므로 activeInHierarchy는 false가 된다. 
                  gameObject.activeSelf    // 부모의 활성여부와 상관없이 오직 자신의 active 상태를 표시한다. 하이어라키상에서 부모에 의해 비활성상태여도 자신은 활성상태라면 activeSelft는 true 이다. */
                //Sets the object as active
                go.SetActive(true);

                //returns the objects
                return go;
            }
        }
        // 풀 안에 우리가 원하는게 없다면, 새로 만들어 줌.
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            // 프리팹들 중에서 체크해봄.
            if (objectPrefabs[i].name == type)
            {
                //We instantiate the prefab of the correct type
                // 있다면 프리팹을 instantiate 해줌. 
                GameObject newObject = Instantiate(objectPrefabs[i]);
                newObject.name = type;
                pooledObjects.Add(newObject);
                return newObject;
            }
        }

        // 아무것도 없다면 null 값을 반환
        return null;
    }
    
    /// Releases the object in the pool
    /// 풀 안에 있는 오브젝트를 Release
    public void ReleaseObject(GameObject gameObject)
    {
        // 오브젝트 Active를 끔.
        gameObject.SetActive(false);
    }
}