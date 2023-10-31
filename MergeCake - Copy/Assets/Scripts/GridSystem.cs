using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GridSystem : MonoBehaviour
{
    public GameObject cellPrefab;
    public float cellSize = 1f;
    public int gridSize = 3;

    private GameObject draggedObject;
    private Vector3 initialObjectPosition;
    private Vector3 offset;
    [SerializeField] LayerMask ignoredLayer;
    [SerializeField] private Text objText;
    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 position = new Vector3(x * cellSize, 0f, z * cellSize);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.transform.localScale = new Vector3(cellSize,  1f, cellSize);
                cell.transform.parent = transform;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;

                if (objectHit.layer == 7)
                {
                    draggedObject = objectHit;
                    initialObjectPosition = draggedObject.transform.position;
                    offset = hit.point - draggedObject.transform.position;
                }
            }
        }
        else if (Input.GetMouseButton(0) && draggedObject != null) //MOVING THE OBJECT
        {
            objText.text = draggedObject.tag;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                Vector3 newPosition = ray.GetPoint(offset.y);
                draggedObject.transform.position = new Vector3(hit.point.x, draggedObject.transform.position.y, hit.point.z);

            }
        }



        else if (Input.GetMouseButtonUp(0) && draggedObject != null) //Drop
        {
            draggedObject.GetComponent<BoxCollider>().enabled = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, ignoredLayer))
            {
                GameObject objectHit = hit.transform.gameObject;

                if (objectHit.tag == "Cell" && objectHit.transform.childCount < 2) //eger tagi celldirse ve cell-in childi yoxdursa
                {
                    draggedObject.transform.position = new Vector3(objectHit.transform.position.x, draggedObject.transform.position.y, objectHit.transform.position.z);
                    draggedObject.transform.SetParent(objectHit.transform);
                }
                else
                {
                    if (draggedObject.tag == objectHit.tag) //eger obyekt varsa ve taglar uygun gelirse
                    {
                        string SpawnObjName = TagManager.Instance.TagReturn(draggedObject.tag);
                        GameObject nextObj = Spawner.Instance.SpawnNewObject(SpawnObjName, draggedObject.transform);
                        nextObj.transform.DOScale(nextObj.transform.localScale.x + .15f, .1f).OnComplete(() => {
                            nextObj.transform.DOScale(nextObj.transform.localScale.x - .15f, .1f);
                        });
                        nextObj.transform.SetParent(objectHit.transform.parent);
                        nextObj.transform.localPosition = new Vector3(0f,.5f,0f);

                        Destroy(draggedObject);
                        Destroy(objectHit);

                    }
                    else
                    {
                        draggedObject.transform.position = initialObjectPosition;
                    }
                }
                //draggedObject.transform.position = initialObjectPosition;

            }
            else
            {
                draggedObject.transform.position = initialObjectPosition;
            }
            draggedObject.GetComponent<BoxCollider>().enabled = true;
            draggedObject = null;
        }
    }
}
