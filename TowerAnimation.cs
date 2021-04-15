using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    private TowerAI tower;
    private Animator animator;

    private void Awake()
    {
        tower = GetComponent<TowerAI> ();
        animator = GetComponent<Animator> ();
    }  

    public void HitAnimation(Transform creep)
    {
        if( IsTargetFoward (creep) )
            animator.Play (Constants.ANIM_TOWER_HIT_FORWARD);
        else
            animator.Play (Constants.ANIM_TOWER_HIT_BACK);
    }

    public void HitAnimation()
    {
        animator.Play (Constants.ANIM_TOWER_HIT_BACK);
    }


    public void AttackAnimation( Creep creep )
    {
        int towerLine = tower.LinePosition;
        int creepLine = creep.GetLinePosition ();
        bool isTargetForward = IsTargetFoward (creep.creepTransform);
        if ( isTargetForward ) // target forward
        {
            animator.Play (Constants.ANIM_TOWER_ATTACK_FORWARD);
        }
        else
        {
            animator.Play (Constants.ANIM_TOWER_ATTACK_BACK);
        }

    }

    private bool IsTargetFoward( Transform creep )
    {
        if ( creep && tower )
            return creep.position.x - tower.towerTransform.position.x <= 0;
        else return false;
      
    }

    public void FaceToAnimation( Transform creep )
    {       
        bool isTargetForward = IsTargetFoward (creep);
        if ( isTargetForward ) // target forward
        {
            animator.SetBool (Constants.ANIM_TOWER_STAND_FORWARD, true);
        }
        else
        {
            animator.SetBool (Constants.ANIM_TOWER_STAND_FORWARD, false);
        }
    }

    public void DefaultAnimation()
    {
        animator.SetBool (Constants.ANIM_TOWER_STAND_FORWARD, false);
    }    

}
