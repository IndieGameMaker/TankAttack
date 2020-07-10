using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;
    public float moveSpeed = 10.0f;
    public float turnSpeed = 100.0f;

    #region UNITY_CALLBACK
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

    }

    /// <summary>
    /// Update 함수 : 프레임마다 호출되는 콜백함수
    /// @param myParam 파라메터 
    /// </summary>
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir * Time.deltaTime * moveSpeed);
        tr.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
    }

    void FixedUpdate()
    {

    }

    void LateUpdate()
    {

    }

    void OnCollistionEnter(Collision coll)
    {

    }

    void OnCollisionStay(Collision coll)
    {

    }
    #endregion

    #region LJH_FUNCTIONS
    /// <summary>
    /// 정수값과 합계값을 반환할 수 있는 함수
    /// </summary>
    /// <param name="i">인티저 값으로 정수를 넘긴다.</param>
    /// <param name="sum">합계값을 전달 한다.</param>
    void MyFunction(int i, float sum)
    {

    }

    void MyFunction2()
    {

    }

    void MyFunction3()
    {

    }

    void MyFunction4()
    {

    }
    void MyFunction5()
    {

    }
    void MyFunction6()
    {

    }
    void MyFunction7()
    {

    }
    void MyFunction8()
    {

    }
    void MyFunction9()
    {

    }

    #endregion
}