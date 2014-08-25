changes = true;
var codigoPlan;
var codigoPlan2;
var contFilas1;
var contFilas2;

//window.onbeforeunload=closeIt;


function closeIt() 
{ 		
	if (changes) 
	{ 
	
		InsertarPlanes();
	} 
}



function ColocaFilasPlan1(codigoPlan,descPlan,postDialogo,varEliminar){
	
	var rowName = jQuery.trim("trPlan1_" + codigoPlan);
	var checkDelId = rowName + "_del";
	var fila = ""; 
	var idTdPostDialogo = jQuery.trim("tdPlan1Post_" + codigoPlan);
	var idChkPostDialogo = "chkPlan1_" + codigoPlan;
	var llamadaEliminar = "javascript:EliminarPlanVarEnfoque('"+ rowName +"')";	
	
	if(contFilas1==1)
	{
	    fila += "<tr id='" + rowName + "' style='border-bottom: solid 1px #ffffff;' class='trVarEnfoques'>";
	}
	else
	{
	    contFilas1=0;
	    fila += "<tr id='" + rowName + "' style='border-bottom: solid 1px #ffffff;' class='trAlternoVarEnfoques'>";
	}
	
	
    fila += "<td style='border-bottom: solid 1px #999999;'><pre><span class='descPlan1'>"+ descPlan + "</span></pre></td>";
	
	if(postDialogo==0)
	{
	    fila += "<td  style='border-bottom: solid 1px #999999;text-align:center' id='"+idTdPostDialogo+"' class='clsChkPlan1'><input id='"+idChkPostDialogo+"' type='checkbox' /></td>";
	}
	else
	{
	    fila += "<td  style='border-bottom: solid 1px #999999;text-align:center' id='"+idTdPostDialogo+"' class='clsChkPlan1'><input id='"+idChkPostDialogo+"' type='checkbox' checked='checked' /></td>";
	}
	if(varEliminar==0)
	{
	    fila += "<td style='border-bottom: solid 1px #999999;text-align:right' ><img class='imgEliminar1' onclick="+llamadaEliminar+" src='../Images/delete_icon.png' title='Eliminar Plan de Accion' /></td>";
	}
	else
	{
	    fila += "<td style='border-bottom: solid 1px #999999;text-align:right' ><img class='imgEliminar1'  src='../Images/delete_icon.png' title='Eliminar Plan de Accion' /></td>";
	}
	
	fila += "</tr>";

	$('#trInicialPlan1').after(fila);
	changes=true;
	    
}

function ColocaFilasPlan2(codigoPlan,descPlan,postDialogo,varEliminar){
	
	var rowName = jQuery.trim("trPlan2_" + codigoPlan);
	var checkDelId = rowName + "_del";
	var fila = ""; 
	var idTdPostDialogo = jQuery.trim("tdPlan2Post_" + codigoPlan);
	var idChkPostDialogo = "chkPlan2_" + codigoPlan;
	var llamadaEliminar = "javascript:EliminarPlanVarEnfoque('"+ rowName +"')";
	
	if(contFilas2==1)
	{
	    fila += "<tr id='" + rowName + "' style='border-bottom: solid 1px #ffffff;' class='trVarEnfoques'>";
	}
	else
	{
	    contFilas2=0;
	    fila += "<tr id='" + rowName + "' style='border-bottom: solid 1px #ffffff;' class='trAlternoVarEnfoques'>";
	}
	
    fila += "<td style='border-bottom: solid 1px #999999;' ><pre><span class='descPlan2'> "+ descPlan + "</span></pre></td>";
    
	if(postDialogo==0)
	{
	    fila += "<td  style='border-bottom: solid 1px #999999;text-align:center' id='"+idTdPostDialogo+"' class='clsChkPlan2'><input id='"+idChkPostDialogo+"' type='checkbox' /></td>";
	}
	else
	{
	    fila += "<td  style='border-bottom: solid 1px #999999;text-align:center' id='"+idTdPostDialogo+"' class='clsChkPlan2'><input id='"+idChkPostDialogo+"' type='checkbox' checked='checked'/></td>";
	}
	if(varEliminar==0)
	{
	    fila += "<td style='border-bottom: solid 1px #999999;text-align:right' ><img class='imgEliminar2' onclick="+llamadaEliminar+" src='../Images/delete_icon.png' title='Eliminar Plan de Accion' /></td>";
	}
	else
	{
	fila += "<td style='border-bottom: solid 1px #999999;text-align:right' ><img class='imgEliminar2'  src='../Images/delete_icon.png' title='Eliminar Plan de Accion' /></td>";
	}
	  
	  
	fila += "</tr>";

	$('#trInicialPlan2').after(fila);
	changes=true;
	    
}
function InsertarPlanes(){

	var n = 0;
	var descPlan;
	var postDialogo;
	var arrPlan1='';
	var arrPostDialogo1='';
	var arrPlan2='';
	var arrPostDialogo2='';
	var arrIDPlanVarEnfoque1='';
	var arrIDPlanVarEnfoque2='';
	
	
	
	n = 0;
	
		
	
	EnviarInformacion(arrPlan1,arrPostDialogo1,arrIDPlanVarEnfoque1,arrPlan2,arrPostDialogo2,arrIDPlanVarEnfoque2);
}


function CargarInformacion1(varIdVariableEnfoque,varEliminar)
{
//var str = jQuery.param(varArrPlan1);
//alert('estamos cargando data de :'+ varIdVariableEnfoque);
if(varIdVariableEnfoque=='')
{
    return;
}
     $.getJSON("../Ajax/AjaxVarEnfoque.ashx",
	        {
	            idVariableEnfoque:varIdVariableEnfoque,
	            mensaje:"",
	            error:"",
	            accion:"cargar"
	        }, 
	         function(json){
	                if(json!='')
	                {
	                    if(json.length > 0){
	                        changes=true;
	                        var ListPlanes = {beVariableEnfoque: json};
	                        for(i = 0;i<ListPlanes.beVariableEnfoque.length;i++)
	                        {
	                            contFilas1=contFilas1+1;
	                            ColocaFilasPlan1(ListPlanes.beVariableEnfoque[i].idVariableEnfoquePlan,ListPlanes.beVariableEnfoque[i].planAccion,ListPlanes.beVariableEnfoque[i].postDialogo,varEliminar);
	                           // alert(ListPlanes.beVariableEnfoque[i].planAccion);
	                        }
				       } 
	                }
	               
	         }
	       );
	       
	       return false;
}


function CargarInformacion2(varIdVariableEnfoque,varEliminar)
{
//var str = jQuery.param(varArrPlan1);
//alert('estamos cargando data de :'+ varIdVariableEnfoque);
if(varIdVariableEnfoque=='')
{
    return;
}
     $.getJSON("../Ajax/AjaxVarEnfoque.ashx",
	        {
	            idVariableEnfoque:varIdVariableEnfoque,
	            mensaje:"",
	            error:"",
	            accion:"cargar"
	        }, 
	         function(json){
	            if(json!='')
	            {
                    if(json.length > 0){
                        changes=true;
	                    var ListPlanes2 = {beVariableEnfoque: json};
	                    for(i = 0;i<ListPlanes2.beVariableEnfoque.length;i++)
	                    {
	                        contFilas2=contFilas2+1;
	                        ColocaFilasPlan2(ListPlanes2.beVariableEnfoque[i].idVariableEnfoquePlan,ListPlanes2.beVariableEnfoque[i].planAccion,ListPlanes2.beVariableEnfoque[i].postDialogo,varEliminar);
	                       // alert(ListPlanes.beVariableEnfoque[i].planAccion);
	                    }
				    }
                }
	         }
	       );
	       
	       return false;
}

function EliminarPlanVarEnfoque(varIdPlanVarEnfoque)
{

 var idPlan=varIdPlanVarEnfoque.split('_');
 idPlan=idPlan[1];
 var inicioIDPlan=idPlan.substring(0,1);
 if(inicioIDPlan=='N')
 {
    EliminarFila(varIdPlanVarEnfoque);
    return;
 }
 $.getJSON("../Ajax/AjaxVarEnfoque.ashx",
	        {
	            idVariablePlanEnfoque:idPlan,
	            mensaje:"",
	            error:"",
	            accion:"eliminar"
	        }, 
	         function(json){
                    
                EliminarFila(varIdPlanVarEnfoque);
	         }
	       );
	       
	       return false;
}

function EliminarFila(nombreFila){
	
	$('#' + nombreFila).remove();
}