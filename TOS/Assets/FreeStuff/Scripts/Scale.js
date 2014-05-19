var pars : ParticleEmitter[];
var scale : float= 1;

@SerializeField
@HideInInspector
private var minsize : float[];

@HideInInspector
@SerializeField
private var maxsize : float[];

@HideInInspector
@SerializeField
private var worldvelocity : Vector3[];

@HideInInspector
@SerializeField
private var localvelocity : Vector3[];

@HideInInspector
@SerializeField
private var rndvelocity : Vector3[];

@HideInInspector
@SerializeField
private var rndforce : Vector3[];

@HideInInspector
@SerializeField
private var forces : Vector3[];


@HideInInspector
@SerializeField
private var scaleBackUp : Vector3[];

@SerializeField
@HideInInspector
private var firstUpdate = true;

function UpdateScale () {   
    var length = pars.length;
    
    if(firstUpdate == true){
	minsize = new float[length];
	
    maxsize = new float[length];
    
    worldvelocity = new Vector3[length];
    
  	localvelocity = new Vector3[length];
  	
   	rndvelocity = new Vector3[length];
   	
   	rndforce = new Vector3[length];
   	
   	forces = new Vector3[length];
   	
    scaleBackUp = new Vector3[length];
    }
      
   
    for (i = 0; i < pars.length; i++) { 
    	var anim = pars[i].gameObject.GetComponent(ParticleAnimator);
    	if(firstUpdate == true){
    		minsize[i] = pars[i].minSize;
        	maxsize[i] = pars[i].maxSize;
        	worldvelocity[i] = pars[i].worldVelocity;
        	localvelocity[i] = pars[i].localVelocity;
        	rndvelocity[i] = pars[i].rndVelocity;
        	rndforce[i] = anim.rndForce;
        	forces[i] = anim.force;
        	scaleBackUp[i] = pars[i].transform.localScale;
        }
        
        pars[i].minSize = minsize[i] * scale;
        pars[i].maxSize = maxsize[i] * scale;
        pars[i].worldVelocity = worldvelocity[i] * scale;
        pars[i].localVelocity = localvelocity[i] * scale;
        pars[i].rndVelocity = rndvelocity[i] * scale;
        anim.rndForce = rndforce[i] * scale;
        anim.force = forces[i] * scale;
        pars[i].transform.localScale = scaleBackUp[i] * scale;
        
    }
	firstUpdate = false;
}

// ParticleAnimator


