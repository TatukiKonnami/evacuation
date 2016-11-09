using UnityEngine;
using System.Collections;

public class HeadLookWalk : MonoBehaviour {
	public float velocity = 0.7f;
	public bool isWalking = false;

	private CharacterController controller;
    /*CharacterController : 衝突判定によって動きの制限を行う*/

    public Clicker clicker = new Clicker();

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
	void Update () {
        if (clicker.clicked()) {
			isWalking = !isWalking; //クリックするたびにtrueとfalseで切り替え
            Debug.Log(this.isWalking);
        }

		if (isWalking) {
            // controller.SimpleMove (Camera.main.transform.forward * velocity);
            controller.SimpleMove(Camera.main.transform.forward * velocity);
            /*SimpleMove : キャラクターを speed 付きで動かせる
             *Camera.main : MainCameraを参照
             *transform.forward : オブジェクトが向いている方向(前面)のベクトルを取得(ワールド空間の Transform の青(Z)軸)
             *velocity : 速度
             */
        }
    }
}
