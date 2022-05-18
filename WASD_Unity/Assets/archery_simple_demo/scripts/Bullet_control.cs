using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace epoching.fps
{

    //初始化子弹
    //子弹飞向的方向
    //检查碰撞
    public class Bullet_control : MonoBehaviour
    {
        [Header("爆炸效果，火球，枪口效果 Explosion effect, fireball, muzzle effect")]
        public GameObject effect_boom;
        public GameObject effect_fireball;
        // public GameObject effect_muzzle;

        [Header("子弹飞行的速度 The speed of the bullet")]
        public float bullet_speed;

        //是否被销毁 Whether to be destroyed
        private bool is_destroy = false;

        //目标打击位置 Target strike position
        private Vector3 target_point;

        // Start is called before the first frame update
        void Start()
        {
            //找到需要打击的点位,并计算出子弹的路径向量,不同的子弹目标点不一样 Find the point that needs to be hit, and calculate the path vector of the bullet, different bullet target points are different
            #region
            //发射子弹 发射一个射线撞到啥子就是啥子 Fire a bullet
            RaycastHit hit;
            int layerMask = 1 << 1;
            layerMask = ~layerMask;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2000, layerMask))
            {
                this.target_point = hit.point;

                //初始化生成火球 Initialize the fireball
                #region 
                this.effect_fireball = Instantiate(this.effect_fireball, transform.position, transform.rotation) as GameObject;
                this.effect_fireball.transform.parent = transform;
                #endregion

                //3秒后自我毁灭 Self-destruct after 3 seconds
                Destroy(this.gameObject, 2f);
            }
            #endregion
        }

        // Update is called once per frame
        void Update()
        {
            if (!this.is_destroy)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.target_point, this.bullet_speed * Time.deltaTime);
            }
        }

        //碰撞检测 Impact checking
        void OnCollisionEnter(Collision collider)
        {
            //return;
            if (collider.gameObject.tag == "boundary")
            {
                Destroy(this.gameObject);
                return;
            }

            if (collider.gameObject.tag == "bullet")
                return;

            if (collider.gameObject.tag == "Player")
                return;

            if (this.is_destroy)
                return;

            //击中敌人 Hit the enemy
            if (collider.gameObject.tag == "enemy")
            {
                collider.gameObject.GetComponent<Enemy_control>().be_hit(1, Camera.main.transform.position);
            }

            //击中油桶 Hit the powder keg
            if (collider.gameObject.tag == "powder_keg")
            {
                collider.gameObject.GetComponent<Powder_keg_control>().be_hit();
            }

            //已经销毁了 Already destroyed
            this.is_destroy = true;

            //生成爆炸效果 Generate explosive effects
            ContactPoint contact = collider.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            GameObject impactP = Instantiate(this.effect_boom);
            impactP.transform.position = pos;
            impactP.transform.localScale = this.transform.localScale * 0.7f;
            Destroy(impactP, 3f);


            //Debug.Log(collider.gameObject);

            Destroy(this.gameObject, 0.1f);
        }
    }

}