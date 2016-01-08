    #pragma strict
    @script RequireComponent(LineRenderer)
var dist : int; //max distance for beam to travel.
var lr : LineRenderer;
var winTag : String; // i was using for minigame, if laser touches this tag , win
var reftag :String; //tag it can reflect off.
var limit : int = 100; // max reflections
private var verti  :int = 1; //segment handler don't touch.
private var iactive :boolean;
private var currot : Vector3;
private var curpos : Vector3;
function Start () {
     
     
}
     
function Update () {
     
    lr.enabled = Input.GetKey(KeyCode.Space);
    if (Input.GetKey(KeyCode.Space)||Input.GetKeyUp(KeyCode.Space)){
        DrawLaser();
    }
}
function DrawLaser()
{
    verti = 1;
    iactive = true;
    currot = transform.right;
    curpos = transform.position;
    lr.SetVertexCount(verti);
    lr.SetPosition(0,transform.position);
     
    while(iactive)
    {
        verti ++;
        var hit : RaycastHit;
        lr.SetVertexCount(verti);
        if (Physics.Raycast(curpos,currot,hit,dist))
        {
            //verti++;
            curpos=hit.point;
            currot = Vector3.Reflect(currot,hit.normal);
            lr.SetPosition(verti-1,hit.point);
            if (hit.transform.gameObject.tag != reftag){
                iactive = false;
            }
        }
        else
        {
            //verti++;
            iactive = false;
            lr.SetPosition(verti-1,curpos+100*currot);
           
        }
        if (verti >limit)
        {
            iactive = false;
        }
       
       
    }
     
     
     
}
