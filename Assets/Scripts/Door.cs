using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public Transform target;
    public bool INorOUT;
    public Sequence doorSequence;
    public BoxCollider box;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorSequence.IsActive())
        {
            this.GetComponent<BoxCollider>().enabled = false;
            box.enabled = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            doorSequence = DOTween.Sequence();
            doorSequence.Append(target.DORotate(INorOUT ? new Vector3(0, 90, 0) : new Vector3(0, -90, 0), 1f)).
                JoinCallback(() =>
                {
                    //ACA IRIA EL SONIDO PARA ABRIR LA PUERTA
                }).
                AppendInterval(2f).
                AppendCallback(() => 
                {
                    //ACA IRIA EL SONIDO PARA CERRAR LA PUERTA
                }).
                Append(target.DORotate(Vector3.zero, 1f)).
                AppendCallback(() => 
                {
                    this.GetComponent<BoxCollider>().enabled = true;
                    box.enabled = true;
                });
        }
    }

}
