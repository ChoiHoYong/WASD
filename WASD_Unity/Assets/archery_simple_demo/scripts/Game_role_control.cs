using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
/// 实现角色的前后左右移动，以及射击
/// </summary>

namespace epoching.fps
{
    public class Game_role_control : MonoBehaviour
    {

        public static Game_role_control instance;

        //-----move------------------------------------
        #region 
        [Header("移动速度 Moving speed")]
        public float speed_forward;
        public float speed_back;
        public float speed_left_right;

        [Header("character controller")]
        public CharacterController character_controller;

        [Header("character animator")]
        public Animator role_animator;

        [Header("audio_source_foot_step")]
        public AudioSource audio_source_foot_step;

        //垂直玩家输入偏移量 Vertical player input offset
        private float vertical_offset;

        //水平玩家输入偏移量 Horizontal player input offset
        private float horizontal_offset;

        //移动方向 Moving direction
        private Vector3 moveDir;

        //游戏角色运动状态
        private Game_role_move_statu game_role_move_statu = Game_role_move_statu.idle;

        #endregion

        //-----fire---------------------------------------------------
        #region 
        [Header("transform_main_camera")]
        public Transform transform_main_camera;

        [Header("impact effect")]
        public Transform bullet_spawn_position;

        [Header("Bullet prefab")]
        public GameObject prefab_bullet;

        [Header("is_firing")]
        public bool is_firing;

        [Header("audio_source_draw_the_bow")]
        public AudioSource audio_source_draw_the_bow;
        #endregion

        //----------------------------
        [Header("falsh blood animator")]
        public Animator animator_flash_blood;

        [Header("health")]
        public int health;

        //init transform
        private Vector3 init_position;
        private Quaternion init_rotation;

        //事件派发器
        public event Handler player_be_killed_event;

        void Awake()
        {
            Game_role_control.instance = this;
        }

        void Start()
        {
            this.init_position = this.transform.position;
            this.init_rotation = this.transform.rotation;
        }

        void Update()
        {
            if (Normal_game_control.instance.game_statu != Game_statu.gaming)
                return;

            //开火 fire
            #region
            //监听鼠标按键 Monitor mouse buttons
            if (Input.GetMouseButtonDown(0))
            {
                //this.is_firing = true;
                //this.time_stamp = Time.time;

                //play animation
                if (this.role_animator.GetCurrentAnimatorStateInfo(1).IsName("aim") == false)
                {
                    this.role_animator.SetBool("is_fire", false);
                    this.role_animator.Play("aim", 1, 0f);
                    this.role_animator.Play("aim", 0, 0f);

                    //play audio
                    this.audio_source_draw_the_bow.Play();

                    this.is_firing = true;
                }


            }
            if (Input.GetMouseButtonUp(0))
            {
                if (this.role_animator.GetCurrentAnimatorStateInfo(1).IsName("aim") == true)
                {
                    //play audio
                    this.audio_source_draw_the_bow.Stop();

                    this.role_animator.SetBool("is_fire", true);

                    StartCoroutine(this.spawn_bullet());

                    this.is_firing = false;
                }
            }
            #endregion
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (Normal_game_control.instance.game_statu != Game_statu.gaming)
                return;

            //获取输入的偏移量 Get the input offset
            this.vertical_offset = Input.GetAxis("Vertical");
            this.horizontal_offset = Input.GetAxis("Horizontal");

            //控制角色动画
            #region 
            if (this.vertical_offset > 0 && (Mathf.Abs(this.vertical_offset) - Mathf.Abs(this.horizontal_offset)) >= 0)
            {
                //向前  forward
                this.change_move_statu(Game_role_move_statu.front);
            }
            else if (this.vertical_offset < 0 && (Mathf.Abs(this.vertical_offset) - Mathf.Abs(this.horizontal_offset)) >= 0)
            {
                //向后 back
                this.change_move_statu(Game_role_move_statu.back);
            }
            else if (this.horizontal_offset > 0 && (Mathf.Abs(this.horizontal_offset) - Mathf.Abs(this.vertical_offset)) > 0)
            {
                //向左 left
                this.change_move_statu(Game_role_move_statu.left);
            }
            else if (this.horizontal_offset < 0 && (Mathf.Abs(this.horizontal_offset) - Mathf.Abs(this.vertical_offset)) > 0)
            {
                //向右 right
                this.change_move_statu(Game_role_move_statu.right);
            }
            else
            {
                //idle
                this.change_move_statu(Game_role_move_statu.idle);
            }
            #endregion

            //控制角色移动
            #region 
            //移动距离,向前和向后的速度不应该一样 Moving distance, forward and backward speed should not be the same
            if (this.game_role_move_statu == Game_role_move_statu.back)
            {
                this.moveDir = this.transform.right * horizontal_offset * this.speed_left_right * Time.deltaTime + this.transform.forward * this.vertical_offset * this.speed_back * Time.deltaTime;//forward back
            }
            else
            {
                this.moveDir = this.transform.right * horizontal_offset * this.speed_left_right * Time.deltaTime + this.transform.forward * this.vertical_offset * this.speed_forward * Time.deltaTime;//forward back
            }
            this.character_controller.SimpleMove(this.moveDir);
            #endregion
        }

        //be_hit
        public void be_hit(int damage)
        {
            if (Normal_game_control.instance.game_statu == Game_statu.gaming)
            {
                this.health -= damage;
                this.flash_blood();

                if (this.health <= 0)
                {
                    this.player_be_killed_event();
                }
            }
        }

        //reset crystal
        public void reset()
        {
            //reset health
            this.health = Config.play_health;

            //reset position and rotation
            this.transform.position = this.init_position;
            this.transform.rotation = this.init_rotation;
            this.transform.Find("Archer_Bindpose").localRotation = Quaternion.Euler(0, 0, 0);

            //this.GetComponent<Rigidbody>().isKinematic = true;

        }

        //flash blood
        private void flash_blood()
        {
            this.animator_flash_blood.Play("flash_blood", 0);
        }

        //切换角色运动状态
        private void change_move_statu(Game_role_move_statu move_statu)
        {
            if (this.game_role_move_statu == move_statu)
                return;

            //audio foot step
            if (move_statu == Game_role_move_statu.idle)
            {
                this.audio_source_foot_step.Stop();
            }
            else
            {
                this.audio_source_foot_step.Play();
            }

            this.role_animator.SetBool("is_moving_front", false);
            this.role_animator.SetBool("is_moving_back", false);
            this.role_animator.SetBool("is_moving_left", false);
            this.role_animator.SetBool("is_moving_right", false);

            switch (move_statu)
            {
                case Game_role_move_statu.front:
                    this.role_animator.Play("front");
                    this.role_animator.SetBool("is_moving_front", true);
                    break;
                case Game_role_move_statu.back:
                    this.role_animator.Play("back");
                    this.role_animator.SetBool("is_moving_back", true);
                    break;
                case Game_role_move_statu.left:
                    this.role_animator.Play("left");
                    this.role_animator.SetBool("is_moving_left", true);
                    break;
                case Game_role_move_statu.right:
                    this.role_animator.Play("right");
                    this.role_animator.SetBool("is_moving_right", true);
                    break;
                default:
                    break;
            }

            this.game_role_move_statu = move_statu;
        }

        //延时生成子弹
        public IEnumerator spawn_bullet()
        {
            yield return new WaitForSeconds(0.05f);
            GameObject arrow = Instantiate(this.prefab_bullet, this.bullet_spawn_position.position, this.bullet_spawn_position.rotation);
            arrow.transform.localScale = new Vector3(5, 5, 5);
        }
    }

    //游戏角色的运动状态
    public enum Game_role_move_statu
    {
        idle,
        front,
        back,
        left,
        right
    }
}