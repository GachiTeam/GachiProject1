using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBehavior : GenericBehavior
{
    //referinte la componente ale obiectului
    private Rigidbody2D mRigidbody;

    //proprietatile fizicii     constante
    private const float m_MinGroundNormalY = .65f;
    private const float m_GravityModifier = 3f;
    private const float m_MinMoveDistance = 0.001f;
    private const float m_ShellRadius = 0.01f;

    //despre stare
    private bool mGrounded;
    private Vector2 mGroundNormal;
    private Vector2 mVelocity;
    private Vector2 mTargetVelocity;

    //containere de informatii
    private ContactFilter2D mContactFilter;
    private RaycastHit2D[] mHitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> mHitBufferList = new List<RaycastHit2D>(16);

    private PhysicsBehavior() {} //disabled default constructor
    //constructor cu setare de referinte
    public PhysicsBehavior(GameObject _gameobject, Rigidbody2D _rigidbody)
    {
        //setez referintele
        mGameObject = _gameobject;
        mRigidbody = _rigidbody;

        //setup pt rigidbody
        mRigidbody.bodyType = RigidbodyType2D.Kinematic;
        //to do vezi de full kinematic #toDo

        //setup the component
        mContactFilter.useTriggers = false;
        mContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(mGameObject.layer));
        mContactFilter.useLayerMask = true;
        mTargetVelocity = Vector2.zero;
    }

    //to be called in Update()
    protected override void UpdateMyBehavior() 
    {}

    //to be called in FixedUpdate()
    protected override void FixedUpdateMyBehavior()
    {
        //setup
        mVelocity += m_GravityModifier * Physics2D.gravity * Time.deltaTime;
        mVelocity.x = mTargetVelocity.x; //nasty
        mGrounded = false;

        HorizontalMovement();
        VerticalMovement();
    }

    void HorizontalMovement()
    {
        Vector2 deltaPosition = mVelocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(mGroundNormal.y, -mGroundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);
    }

    void VerticalMovement()
    {
        Movement(Vector2.up * mVelocity.y * Time.deltaTime, true);
    }

    void Movement(Vector2 _move, bool _yMovement)
    {
        float distance = _move.magnitude;

        if (distance > m_MinMoveDistance)
        {
            UpdateBufferList(_move, distance);
            UpdateDistance(ref distance, _yMovement);
            Move(_move, distance);
        }
    }

    void Move(Vector2 _move, float _distance)
    {
        mRigidbody.position = mRigidbody.position + _move.normalized * _distance;
    }

    void UpdateDistance(ref float distance, bool _yMovement)
    {
        for (int i = 0; i < mHitBufferList.Count; ++i)
        {
            Vector2 currentNormal = mHitBufferList[i].normal;

            if (currentNormal.y > m_MinGroundNormalY)
            {
                mGrounded = true;

                if (_yMovement)
                {
                    mGroundNormal = currentNormal;
                    currentNormal.x = 0;
                }
            }

            float projection = Vector2.Dot(mVelocity, currentNormal);
            if (projection< 0)
            {
                mVelocity = mVelocity - projection* currentNormal;
            }

            float modifiedDistance = mHitBufferList[i].distance - m_ShellRadius;
            distance = (modifiedDistance < distance) ? modifiedDistance : distance;
        }
    }

    void UpdateBufferList(Vector2 _move, float _distance)
    {
        int count = mRigidbody.Cast(_move, mContactFilter, mHitBuffer, _distance + m_ShellRadius);

        mHitBufferList.Clear();

        for (int i = 0; i < count; i++)
        {
            mHitBufferList.Add(mHitBuffer[i]);
        }
    }

    public void SetTargetVelocity(Vector2 _targetVelocity)
    {
        mTargetVelocity = _targetVelocity;
    }

    public void Jump(float _input)
    {
        if (mGrounded == true)
        {
            mVelocity.y = _input;
        }
    }

    public void SetMoving(float _direction, float _maxSpeed)
    {
        Vector2 move = Vector2.zero;
        move.x = _direction;
        
        mTargetVelocity= move * _maxSpeed;

    }

    public Vector2 GetVelocity()
    {
        return mVelocity;
    }
}
