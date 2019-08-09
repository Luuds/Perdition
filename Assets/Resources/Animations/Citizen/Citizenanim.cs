
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Spine;
using Spine.Unity; 

public class Citizenanim : MonoBehaviour
{
    // Start is called before the first frame update
   
    GameObject player;
    NavMeshAgent agent;
    public float velocity;
    Animator anim;
    public bool turnLeft; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = player.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        velocity = agent.velocity.x;
        if (velocity > 0f ){
            anim.SetBool("Stop", false);
            anim.SetFloat("Velocity", velocity/agent.speed);
            turnLeft = false;
            GetComponent<SkeletonMecanim>().Skeleton.ScaleX = 1;
        }
        else if (velocity < 0f )
        {
            anim.SetBool("Stop", false);
            anim.SetFloat("Velocity", -(velocity / agent.speed));
           turnLeft = true;
            GetComponent<SkeletonMecanim>().Skeleton.ScaleX = -1; 
        }
        else if (velocity == 0f)
        {
            anim.SetFloat("Velocity", 0);
            anim.SetBool("Stop",true);
           if (turnLeft == true) {
            GetComponent<SkeletonMecanim>().Skeleton.ScaleX = -1;
             
            }
            else if (turnLeft != true)
            {
                GetComponent<SkeletonMecanim>().Skeleton.ScaleX = 1;
             
            }
        }
           


    }
}
/*[SpineAnimation]
public string animTrans;
GameObject player;
NavMeshAgent agent;
public float velocity;

SkeletonAnimation skeletonAnimation;
TrackEntry track;


void Start()
{
    player = GameObject.FindGameObjectWithTag("Player");
    agent = player.GetComponent<NavMeshAgent>();
    skeletonAnimation = GetComponent<SkeletonAnimation>();
    track = skeletonAnimation.state.SetAnimation(1, "Run", true);
}

// Update is called once per frame
void Update()
{
    velocity = agent.velocity.x;
    if (velocity > 0f)
    {
        skeletonAnimation.state.SetAnimation(0, "Walk", true);
        track.alpha = velocity / agent.speed;

    }
    else if (velocity < 0f)
    {
        skeletonAnimation.state.SetAnimation(0, "Walk", true);
        track.alpha = velocity / agent.speed;
    }
    else if (velocity == 0f)
    {
        skeletonAnimation.state.SetAnimation(0, "Idle", true);

    }
}*/