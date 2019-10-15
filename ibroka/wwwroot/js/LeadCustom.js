function validateAlphaNumericOnKeyPress(event) {
    var validChars = /[ A-Za-z0-9]/;

    var keyChar = String.fromCharCode(event.which || event.keyCode);
    return validChars.test(keyChar) ? keyChar : false;
}


function updateLeadData() {
  
    $('input').attr('readonly', false);
    $('select').attr('readonly', false);
    //$('#idLeadSubmit').show();
    $('#idLead_Submit').show();
    $('#btnShowNextAgent').show();
    $('#btnUpdateLead').hide();
    $('select').removeAttr('disabled');
    $('#ddlPC').attr('readonly', true);

    $('#ddlPC').prop('disabled', true);
    $('#btnAddDocument').prop('disabled', false);
    
    $('#txtNetPremium').attr('readonly', true);

    checkandSetAgentsProportion('editLead');
}


function convertNumberToWords(n) {

    var string = n.toString(), units, tens, scales, start, end, chunks, chunksLen, chunk, ints, i, word, words, and = 'and';

    /* Remove spaces and commas */
    string = string.replace(/[, ]/g, "");

    /* Is number zero? */
    if (parseInt(string) === 0) {
        return 'zero';
    }

    /* Array of units as words */
    units = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen', 'Eighteen', 'Nineteen'];

    /* Array of tens as words */
    tens = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

    /* Array of scales as words */
    scales = ['', 'Thousand', 'Million', 'Billion', 'Trillion', 'Quadrillion', 'Quintillion', 'Sextillion', 'Septillion', 'octillion', 'nonillion', 'decillion', 'undecillion', 'duodecillion', 'tredecillion', 'quatttuor-decillion', 'quindecillion', 'sexdecillion', 'septen-decillion', 'octodecillion', 'novemdecillion', 'vigintillion', 'centillion'];

    /* Split user arguemnt into 3 digit chunks from right to left */
    start = string.length;
    chunks = [];
    while (start > 0) {
        end = start;
        chunks.push(string.slice((start = Math.max(0, start - 3)), end));
    }

    /* Check if function has enough scale words to be able to stringify the user argument */
    chunksLen = chunks.length;
    if (chunksLen > scales.length) {
        return '';
    }

    /* Stringify each integer in each chunk */
    words = [];
    for (i = 0; i < chunksLen; i++) {

        chunk = parseInt(chunks[i]);

        if (chunk) {

            /* Split chunk into array of individual integers */
            ints = chunks[i].split('').reverse().map(parseFloat);

            /* If tens integer is 1, i.e. 10, then add 10 to units integer */
            if (ints[1] === 1) {
                ints[0] += 10;
            }

            /* Add scale word if chunk is not zero and array item exists */
            if ((word = scales[i])) {
                words.push(word);
            }

            /* Add unit word if array item exists */
            if ((word = units[ints[0]])) {
                words.push(word);
            }

            /* Add tens word if array item exists */
            if ((word = tens[ints[1]])) {
                words.push(word);
            }

            /* Add 'and' string after units or tens integer if: */
            if (ints[0] || ints[1]) {

                /* Chunk has a hundreds integer or chunk is the first of multiple chunks */
                if (ints[2] || !i && chunksLen) {
                    //words.push(and);
                }

            }

            /* Add hundreds word if array item exists */
            if ((word = units[ints[2]])) {
                words.push(word + ' Hundred');
            }

        }

    }

    return words.reverse().join(' ');

}

function createEndorsement(pid, tpe) {
   
    var targeturl = "/Policy/CreateEndorsement/?pid="+ pid;

    $.ajax({
        url: targeturl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {

            $('#dialogEndorse').html(response);
            $('#endorseModal').show();


        },
        failure: function (response) {
            
            alert(response.responseText);
        },
        error: function (response) {
           
            alert(response.responseText);
        }
    });
}

function closeAddEndorsementView() {
    $('#endorseModal').modal('hide');
    $('input').attr('readonly', false);
    $('select').attr('readonly', false);
    $('select').prop('disabled', false);
}

function validateEndorsementData(ltype) {

    var isDataValid = true;
   
    if ($('#idStartDate').val() === '' || $('#idStartDate').val() === undefined || $('#idStartDate').val() === null) {
        isDataValid = false;
        $('#idStartDate').addClass('error');
    } else { $('#idStartDate').removeClass('error'); }
    if ($('#idEndDate').val() === '' || $('#idEndDate').val() === undefined || $('#idEndDate').val() === null) {
        isDataValid = false;
        $('#idEndDate').addClass('error');
    } else {
        $('#idEndDate').removeClass('error');
    }
    
    if ($('#txtCommission').val() === '' || $('#txtCommission').val() === undefined || $('#txtCommission').val() === null || $('#txtCommission').val() === '0.00') {
        isDataValid = false;

        $('#txtCommission').addClass('error');
    } else {

        $('#txtCommission').removeClass('error');
    }
    if ($('#txtGP').val() === '' || $('#txtGP').val() === undefined || $('#txtGP').val() === null || $('#txtGP').val() === '0.00') {
        isDataValid = false;
        $('#txtGP').addClass('error');
    } else {
        $('#txtGP').removeClass('error');
    }

    if ($('#txtDescription').val() === '' || $('#txtDescription').val() === undefined || $('#txtDescription').val() === null) {
        isDataValid = false;
        $('#txtDescription').addClass('error');
    } else {
        $('#txtDescription').removeClass('error');
    }

    if (($('#idEndDate').val() !== '' && $('#idEndDate').val() !== undefined && $('#idEndDate').val() !== null) && ($('#idStartDate').val() !== '' && $('#idStartDate').val() !== undefined && $('#idStartDate').val() !== null)) {

        var stDate = $('#idStartDate').val();
        var eDate = $('#idEndDate').val();
        if (new Date(eDate) <= new Date(stDate)) {
            alert('End date should be greater then start date');
            isDataValid = false;
        }
    }

    if (!isDataValid) {
        alert('Please provide the required data');
        return;
    }
    var sumInsured_val = $('#txtSumInsured').val();
    var grossP_val = $('#txtGP').val();
    var comm_val = $('#txtCommission').val();

    if ((sumInsured_val !== "" && sumInsured_val !== null && sumInsured_val !== undefined) && (grossP_val !== "" && grossP_val !== null && grossP_val !== undefined)) {

        grossP_val = parseFloat(grossP_val);
        sumInsured_val = parseFloat(sumInsured_val);
        //if (grossP_val > sumInsured_val) {
        //    //alert('Gross premium cannot be more than sum insured');
        //    kendo.alert("Gross premium cannot be more than sum insured");
        //    isDataValid = false;
        //    return;
        //}
    }

    if ((grossP_val !== "" && grossP_val !== null && grossP_val !== undefined) && (comm_val !== "" && comm_val !== null && comm_val !== undefined)) {

        grossP_val = parseFloat(grossP_val);
        comm_val = parseFloat(comm_val);
        if (comm_val > grossP_val) {
            alert('Commission cannot be more than gross premium');
            isDataValid = false;
            return;
        }
    }

    if (isDataValid) {
        if (ltype === 'new')
            $('#btnAddEndorsement').click();
        else
            $('#btnUpdateEndorsement').click();
    }
    else {
        // alert('Please provide the required data');
    }
}


function leadEndorsementGPOnChange(el) {

    var v = parseFloat(el.value);
    el.value = (isNaN(v)) ? '' : v.toFixed(2);
    calculateNetPerimumLeadEndorsememnt();
}


function leadEndorsementCommOnChange(el) {

    var v = parseFloat(el.value);
    el.value = (isNaN(v)) ? '' : v.toFixed(2);
    calculateNetPerimumLeadEndorsememnt();
}

function calculateNetPerimumLeadEndorsememnt() {
  
    var gp_val = $('#txtGP').val();
  var comm_val = $('#txtRiskOwnedPercent').val();
  var net_val = 0;
  var com_amount = 0;
  
    if (!isNaN(gp_val) && gp_val!=='') {
        net_val = parseFloat(gp_val);
    }

  if (!isNaN(comm_val) && comm_val !== '') {

    com_amount = parseFloat(gp_val) * parseFloat(comm_val) / 100;
    var vat = com_amount * 5 / 100;

    net_val = parseFloat(net_val) - com_amount - vat;
    }
  net_val = parseFloat(net_val);

  $('#txtCommission').val(com_amount.toFixed(2));
   $('#txtNetPremium').val(net_val.toFixed(2));
}


function showNext_Agent() {
    if ($("#agent_2").is(":hidden")) {
        $("#agent_2").show();
    } else if ($("#agent_3").is(":hidden")) {
        $("#agent_3").show();
    } else if ($("#agent_4").is(":hidden")) {
        $("#agent_4").show();
    } else if ($("#agent_5").is(":hidden")) {
        $("#agent_5").show();
        $('#btnShowNextAgent').hide();
    }

    if ($("#agent_2").is(":visible") && $("#agent_3").is(":visible") && $("#agent_4").is(":visible") && $("#agent_5").is(":visible")) {
        $('#btnShowNextAgent').hide();
    }
}

function removeAgent(idx) {

    if (idx === 'agent_2') {
        $('#Agent2').val('');
        $('#prop2').val('');

    } else if (idx === 'agent_3') {
        $('#Agent3').val('');
        $('#prop3').val('');
    } else if (idx === 'agent_4') {
        $('#Agent4').val('');
        $('#prop4').val('');
    } else if (idx === 'agent_5') {
        $('#Agent5').val('');
        $('#prop5').val('');
    }

    $("#" + idx).hide();
    $('#btnShowNextAgent').show();
}

function loadEndorsementView(id) {

    var targeturl = '/Policy/GetEndorsementById/?id=' + id; //'@Url.Action("GetLeadPolicyData", "PolicyLead")?lid=' + lid;

    $.ajax({
        url: targeturl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            
            $('#dialogEndorse').html(response);
            $('#endorseModal').show();


        },
        failure: function (response) {
           
            alert(response.responseText);
        },
        error: function (response) {
           
            alert(response.responseText);
        }
    });

}

function editLeadEndorsementData() {

    $('input').attr('readonly', false);
    $('select').attr('readonly', false);
    $('select').prop('disabled', false);
    $('#btnEditData').show();
    $('#btnEdit').hide();
    $('#txtNetPremium').attr('readonly', true);
}

function loadEndorsementDebitNoteView(leid) {

    var targeturl = '/Policy/GenerateEndorseDebitNote/?leid=' + leid;

    $.ajax({
        url: targeturl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {

            $('#dialogDebitEndorse').html(response);
            $('#endorseDebitModal').show();


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });

}

function closeEndorseDebitNotePopup() {
    $('#endorseDebitModal').modal('hide');
}

function closeEndorseDebitNote() {
    $('#endorseDebitModal').modal('hide');
}


$(document).delegate('#btnEndorsePayment',
    'click',
    function (e) {
        debugger;
       var pLeadtId = $(this).attr('EditLeadtId');
       var pDebitNoteId = $(this).attr('EditDebitNoteId');
       var pDebitNoteNo = $(this).attr('EditDebitNoteNo');
       var pDebitAmount = $(this).attr('EditDebitAmount');


        $('#pLeadtIdEndorse').val(pLeadtId);
        $('#pDebitNoteIdEndorse').val(pDebitNoteId);
        $('#pDebitNoteNoEndorse').val(pDebitNoteNo);
        $('#pDebitAmountEndorse').val(pDebitAmount);
        $("#pDebitAmount").attr("disabled", "disabled");
        $("#pDebitNoteNo").attr("disabled", "disabled");


    });


function generateEndorsePayment() {

    if ($('#txtPaymentAmountEndorse').val() === "" || $('#txtPaymentAmountEndorse').val() === undefined || $('#txtPaymentAmountEndorse').val() === null) {
        alert('Please provide amount.');
        return;
    }

    if ($('#txtPaymentDateEndorse').val() === "" || $('#txtPaymentDateEndorse').val() === undefined || $('#txtPaymentDateEndorse').val() === null) {
        alert('Please provide payment date.');
        return;
    }

    var pAmount_val = $('#txtPaymentAmountEndorse').val();
    var dAmount_val = $('#pDebitAmountEndorse').val();

    pAmount_val = parseFloat(pAmount_val);
    dAmount_val = parseFloat(dAmount_val);

    if (pAmount_val !== dAmount_val) {

        if (confirm('The given amount is not matching with debit amount. \nAre you sure you want to save this?')) {
            var test = "";
        } else {
            return;
        }
    }

    var LeadPaymentDetail = {

        debitNoteNo: $("#pDebitNoteNoEndorse").val(),
        DebitNoteId: $("#pDebitNoteIdEndorse").val(),
        LeadEndorsementId: $("#pLeadtIdEndorse").val(),
        DateOfPayment: $("#txtPaymentDateEndorse").val(),
        Amount: $("#txtPaymentAmountEndorse").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Policy/SaveLeadEndorsementPaymentDetails",
        data: JSON.stringify(LeadPaymentDetail),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',

        success: function (r) {

            if (r.msg === "Success") {
               
                $('#paymentModal').modal('hide');               
                generateEndorsementPayment($("#pLeadtIdEndorse").val(), 'true');

            } else if (r.msg === "Fail") {

                alert(r.msg);

            } else if (r.msg === "No Data") {
                alert(r.msg);

            }
        },
        error: function () {
            alert(r.msg);
        }
    });
}


function generateEndorsementPayment(leid, isnew) {

    var targeturl = '/Policy/GenerateLeadEndorsementPayment/?leid=' + leid + "&isnew=" + isnew;

    $.ajax({
        url: targeturl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $('#btnEndorsepaymentTrigger').click();
            $('#dialogDebitEndorse').html(response);
            $('#endorseDebitModal').show();


        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function loadInsurerAddModal() {

    var targeturl = '/Insurer/AddInsurer';

    $.ajax({
        url: targeturl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
           
            $('#dialogInsurer').html(response);
            $('#addInsurerModal').show();


        },
        failure: function (response) {
            
            alert(response.responseText);
        },
        error: function (response) {
          
            alert(response.responseText);
        }
    });

}

function closeAddInsurer() {
    $('#addInsurerModal').modal('hide');
    $('input').attr('readonly', false);
}


function validateInsurerData(insType) {

    var isDataValid = true;

    if ($('#txtName').val() === '' || $('#txtName').val() === null || $('#txtName').val() === undefined) {
        isDataValid = false;
        $('#txtName').addClass('error');
    } else {
        $('#txtName').removeClass('error');
    }

    if ($('#txtDisplayName').val() === '' || $('#txtDisplayName').val() === null || $('#txtDisplayName').val() === undefined) {
        isDataValid = false;
        $('#txtDisplayName').addClass('error');
    } else {
        $('#txtDisplayName').removeClass('error');
    }

    if ($('#txtCommission').val() === '' || $('#txtCommission').val() === null || $('#txtCommission').val() === undefined) {
        isDataValid = false;
        $('#txtCommission').addClass('error');
    } else {
        $('#txtCommission').removeClass('error');
    }

    if ($('#txtEmail').val() !== '' && $('#txtEmail').val() !== null && $('#txtEmail').val() !== undefined) {
        
        var isvalid = ValidateEmail($('#txtEmail').val());
        if (isvalid) {
            $('#txtEmail').removeClass('error');
        } else {
            isDataValid = false;
            $('#txtEmail').addClass('error');
        }
    }
    
    if (!isDataValid) {
        return;
    } else {
        if (insType === 'new')
            $('#btnAddInsurer').click();
        else
            $('#btnEditInsurer').click();
    }

}

function ValidateEmail(mail) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
        return true;
    }
    alert("You have entered an invalid email address!");
    return false;
}



function loadInsurerDataView(id) {

    var targeturl = '/Insurer/GetInsurerById/?id=' + id;

    $.ajax({
        url: targeturl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {

            $('#dialogInsurer').html(response);
            $('#addInsurerModal').show();

            $('input').attr('readonly', true);

        },
        failure: function (response) {

            alert(response.responseText);
        },
        error: function (response) {

            alert(response.responseText);
        }
    });

}


function editInsurer() {
    $('input').attr('readonly', false);
    $('#btnInsurerEdit').hide();
    $('#btnValidateInsurer').show();
    
}

function checkDuplicateInsurer() {
    var insurerMaster = {

        Id: $("#txtId").val(),
        Name: $("#txtName").val(),
        OrganisationId: $("#txtOrgId").val(),
        NewRecord: $("#txtRecordType").val(),
        
    };
   
    $.ajax({
        type: "POST",
        url: "/Insurer/CheckInsurerExist",
        data: JSON.stringify(insurerMaster),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',

        success: function (data) {

            
        },
        error: function () {
            
        }
    });
}


function onLeadDataBound(e) {

    var grid = $("#dvLeads").data("kendoGrid");
    var gridData = grid.dataSource.view();

    for (var i = 0; i < gridData.length; i++) {
        if (gridData[i].leadType === 'Endorsement') {
            grid.table.find("tr[data-uid='" + gridData[i].uid + "']").addClass("highlighted-row");
        }
    }
}

function bindPolicyGrid() {
   
    var clientId = document.getElementById("hdnLeadClientId").value;
    $("#dvPolicy").kendoGrid({
        dataSource: {

            transport: {
                read: {
                    url: "/client/GetPolicyByClient/?clientId=" + clientId,
                    type: "get",

                    // the data type of the returned result.
                    datatype: "json"
                }
            },

            schema: {
                model: {
                    fields: {
                        policyName: { type: "string" },
                        startDate: { type: "date" },
                        endDate: { type: "date" },
                        grossPremium: { type: "decimal" },
                        commission: { type: "decimal" },
                        insurerName: { type: "string" },
                        id: { type: "guid" },
                        clientId: { type: "guid" },
                        payment_Id: { type: "string" },
                        debitNote_Id: { type: "string" }
                    }
                }
            },
            pageSize: 10

        },


        filterable: true,
        sortable: true,
        pageable: true,
        detailTemplate: kendo.template($("#policytemplate").html()),
        detailInit: detailInit,
        
        columns: [

            {
                field: "policyName",
                title: "Policy Name"
            },

            {
                field: "startDate",
                title: "Start Date",
                template: "#= kendo.toString(startDate, 'dd/MM/yyyy') #",
                width: 120
            },

            {
                field: "endDate",
                title: "End Date",
                template: "#= kendo.toString(endDate, 'dd/MM/yyyy') #",
                width: 120
            },


            {
                field: "grossPremium",
                title: "Gross Premium",
                format: "{0:n2}"

            },
            {
                field: "commission",
                title: "Brokerage Commission",
                format: "{0:n2}"

            },
            {
                field: "insurerName",
                title: "Insurer"

            },
            {
                title: "Action ",
                field: "id",
                //template:  "<a  class=\"btn btn-sm btn-info btn-icon btn-round\" title=\" View Details\" data-toggle=\"modal\" data-target=\"\\#noticeModal23\" rel=\"tooltip\" data-original-title=\"View Details\"  onclick=\"loadDetailsView('#= id #')\" data-placement=\"top\"><i class=\"fa fa-eye\"></i></a>",
                template: 
                    "<a  class=\"btn btn-sm btn-info btn-icon btn-round\" onclick=\"loadPolicyDetailsView('#=leadId#')\"  data-toggle=\"modal\" data-target=\"\\#noticeModal23\" title=\" View Details\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"View Details\"><i class=\"fa fa-eye\"></i>    </a>"+
                    "<a  class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"loadDebitNoteView('#=leadId#')\"  data-toggle=\"modal\" data-target=\"\\#noticeModal23\" title=\"Generate Debit Note\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Generate Debit Note\"><i class= \"far fa-file-alt\" ></i>    </a >"+

                    "<a  class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"generateLeadPayment('#=leadId#', 'false')\"  data-toggle=\"modal\" data-target=\"\\#noticeModal23\" title=\" Generate Payment Receipt\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Generate Payment Receipt\">  <i class=\"fas fa-dollar-sign\"></i>    </a>"+

                    "<a class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"loadCreditNoteView('#=leadId#')\"  data-toggle=\"modal\" data-target=\"\\#noticeModal23\" title=\"Generate Credit Note\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Generate Credit Note\">   <i class=\"fas fa-file-alt\"></i> </a>"+
                    "# if (creditNote_Id !='') { #" +
                    "<a  class=\"btn btn-round btn-warning btn-icon btn-sm\" id=\"btnUpdatePolicyNo\"  data-toggle=\"modal\" data-target=\"\\#policyNoModal\" EditPolicyId=\"#=id#\" EditPolicyIdNo=\"#=policyNo#\" EditReceiptNoImgUrl=\"#=receiptImgUrl#\" title=\"Update Policy No\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Update Policy No\">  <i class=\"fa fa-edit\"></i></a>" +
                    "# }#" +

                    "# if (policyNo !='') { #"+
                    "<a class=\"btn btn-round btn-warning btn-icon btn-sm\" id=\"btnUploadReceipt\" data-toggle=\"modal\" data-target=\"\\#policyReceiptModal\" EditPolicyId=\"#=id#\" EditPolicyIdNo=\"#=policyNo#\" EditReceiptImgUrl=\"#=receiptImgUrl#\" EditReceiptNo=\"#=receiptNo#\" EditReceiptDate=\"#=receipt_Date#\" title=\"Upload Policy Receipt\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Upload Policy Receipt\"> <i class=\"fas fa-upload\"></i> </a>"+
                    "# }#"+

                    "# if (receiptImgUrl !='') { #" +
                        "<a class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"createEndorsement('#=id#', 'false')\" data-toggle=\"modal\" data-target=\"\\#endorseModal\" title=\"Create endorsement\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Create endorsement\">      <i class=\"fas fa-plus\"></i>  </a>"+
                "# }#"
                     ,
                width: 250,
                sortable: false,
                filterable: false
            }

        ]

    });
}


function detailInit(e) {
    
    var clientId = document.getElementById("hdnLeadClientId").value;
    var detailRow = e.detailRow;
    var id = e.data.id;
    detailRow.find(".dvEndorsements").kendoGrid({
        dataSource: {

            transport: {
                read: {
                    url: "/client/GetEndorsementByPolicyId/?pid=" + id,
                    type: "get",

                    // the data type of the returned result.
                    datatype: "json"
                }
            },

            schema: {
                model: {
                    fields: {
                        policyName: { type: "string" },
                        startDate: { type: "date" },
                        endDate: { type: "date" },
                        grossPremium: { type: "decimal" },
                        commission: { type: "decimal" },
                        insurerName: { type: "string" },
                        id: { type: "guid" },
                        clientId: { type: "guid" },
                        payment_Id: { type: "string" },
                        debitNote_Id: { type: "string" }
                    }
                }
            },
            pageSize: 10

        },


        filterable: true,
        sortable: true,
        pageable: true,
        
        columns: [

            {
                field: "policyName",
                title: "Policy Name"
            },

            {
                field: "startDate",
                title: "Start Date",
                template: "#= kendo.toString(startDate, 'dd/MM/yyyy') #",
                width: 120
            },

            {
                field: "endDate",
                title: "End Date",
                template: "#= kendo.toString(endDate, 'dd/MM/yyyy') #",
                width: 120
            },


            {
                field: "grossPremium",
                title: "Gross Premium",
                format: "{0:n2}"

            },
            {
                field: "commission",
                title: "Brokerage Commission",
                format: "{0:n2}"

            },
            {
                field: "insurerName",
                title: "Insurer"

            },
            {
                title: "Action ",
                field: "id",
                
                template:
                    "<a  class=\"btn btn-sm btn-info btn-icon btn-round\" title=\" View Details\" data-toggle=\"modal\" data-target=\"\\#endorseModal\" rel=\"tooltip\" data-original-title=\"View Details\"  onclick=\"loadEndorsementView('#= leadId #')\" data-placement=\"top\"><i class=\"fa fa-eye\"></i></a>" +
                    "<a class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"loadEndorsementDebitNoteView('#=leadId#')\" data-toggle=\"modal\" data-target=\"\\#endorseDebitModal\" title=\"Generate Debit Note\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Generate Debit Note\"> <i class=\"far fa-file-alt\"></i> </a>" +
                    "<a class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"generateEndorsementPayment('#=leadId#', 'false')\" data-toggle=\"modal\" data-target=\"\\#endorseDebitModal\" rel=\"tooltip\" data-original-title=\" Generate Payment Receipt\" title=\" Generate Payment Receipt\" data-placement=\"top\">  <i class=\"fas fa-dollar-sign\"></i>    </a>" +

                    "<a class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"loadEndorsementCreditNoteView('#=id#')\"  data-toggle=\"modal\" data-target=\"\\#endorseDebitModal\" title=\"Generate Credit Note\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Generate Credit Note\">   <i class=\"fas fa-file-invoice\"></i> </a>" +
                    "<a  class=\"btn btn-round btn-warning btn-icon btn-sm\" id=\"btnUpdateEndorseNo\" data-toggle=\"modal\" data-target=\"\\#endorseNoModal\" EditEndorseId=\"#=id#\" EditEndorseNo=\"#=endorsementNo#\" EditClientId=\"#=clientId#\" EditReceiptNoImgUrl=\"#=receiptImgUrl#\" title=\"Update Endorsement No\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Update Endorsement No\">  <i class=\"fa fa-edit\"></i></a>" +

                    "# if (endorsementNo !='') { #" +
                        "<a class=\"btn btn-round btn-warning btn-icon btn-sm\" id=\"btnUploadEReceipt\" data-toggle=\"modal\" data-target=\"\\#endorsementReceiptModal\" EditEndorseId=\"#=id#\" EditEndorseNo=\"#=endorsementNo#\" EditReceiptImgUrl=\"#=receiptImgUrl#\" EditReceiptNo=\"#=receiptNo#\" EditReceiptDate=\"#=receipt_Date#\" title=\"Upload Endorsement Receipt\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Upload Endorsement Receipt\"> <i class=\"fas fa-upload\"></i> </a>" +
                    "# }#" 

                    //"# if (receiptImgUrl !='') { #" +
                    //"<a class=\"btn btn-round btn-warning btn-icon btn-sm\" onclick=\"createEndorsement('#=id#', 'false')\" data-toggle=\"modal\" data-target=\"\\#endorseModal\" title=\"Create endorsement\" data-placement=\"top\" rel=\"tooltip\" data-original-title=\"Create endorsement\">      <i class=\"fas fa-plus\"></i>  </a>" +
                    //"# }#"
                ,
                width: 250,
                sortable: false,
                filterable: false
            }

        ]

    });
}


function closeEndorsePayment() {
    $('#endorseDebitModal').modal('hide');
}

function bindPolicyDashboard() {
    $("#dv_Policy").kendoGrid({
        dataSource: {

            transport: {
                read: {
                    url: "/Policy/GetPolicyData",
                    type: "get",
                    datatype: "json"
                }
            },

            schema: {
                model: {
                    fields: {
                        name: { type: "string" },
                        age: { type: "number" },
                        phoneNo: { type: "string" },
                        email: { type: "string" },
                        type: { type: "string" },
                        dateCreated: { type: "date" },
                        id: { type: "guid" },
                        clientId: { type: "guid" }
                    }
                }
            },
            pageSize: 10

        },


        filterable: true,
        sortable: true,
        pageable: true,
       
        columns: [

            {
                field: "name",
                title: "Name"
            },

            {
                field: "age",
                title: "Age(Yrs)",
                width: 120
            },

            {
                field: "phoneNo",
                title: "PhoneNo"
            },


            {
                field: "email",
                title: "Email",
                sortable: false
            },
            {
                field: "policyName",
                title: "Class"

            },
            {
                field: "insurerName",
                title: "Insurer"

            },
            {
                field: "type",
                title: "Type"
            },
            {
                field: "sumInsured",
                title: "Sum Assured",
                format: "{0:n2}"
            },
            {
                field: "leadType",
                title: "Lead Type"
            },
            {
                field: "dateCreated",
                title: "Date",
                template: "#= kendo.toString(dateCreated, 'dd/MM/yyyy') #",
                width: 120

            },

            {
                title: " ",
                field: "id",
                template: "<a  class=\"btn btn-sm btn-info btn-icon btn-round\" title=\" View Details\" data-toggle=\"tooltip\" href=\"/Client/Details/#= clientId # \"data-placement=\"top\"><i class=\"fa fa-eye\"></i></a>",
                width: 50,
                sortable: false,
                filterable: false
            },

        ]

    });

    $('#spnLoading').hide();
}


function loadEndorsementCreditNoteView(id) {

    var targeturl = '/Policy/GetEndorsementCreditNoteData/?id=' + id; //'@Url.Action("GetPolicyCreditNoteData", "Policy")?lid=' + lid;

    $.ajax({
        url: targeturl,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {

            $('#dialogDebitEndorse').html(response);
            $('#endorseDebitModal').show();


        },
        failure: function (response) {

            alert(response.responseText);
        },
        error: function (response) {

            alert(response.responseText);
        }
    });

}

function UpdateEndorsementNo() {

    var cid = $("#txtendorseClientId").val();

if ($('#txtEndorsementNo').val() === "" || $('#txtEndorsementNo').val() === undefined || $('#txtEndorsementNo').val() === null) {
    alert('Please provide endorsement no.');
        return;
    }

    var Endorsement = {

        Id: $("#txtendorsementId").val(),
        EndorsementNo: $("#txtEndorsementNo").val()
       
    };    

    $.ajax({
        type: "POST",
        url: "/Policy/SaveEndorsementNo",
        data: JSON.stringify(Endorsement),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',

        success: function (r) {

            if (r.msg === "Success") {
                debugger;
                $('#endorseNoModal').modal('hide');
                 cid = $("#txtendorseClientId").val();
                window.location.href = '/Client/Details/?id=' + cid + '&IsOpen=policies';

            } else if (r.msg === "Fail") {

                alert(r.msg);

            } else if (r.msg === "No Data") {
                alert(r.msg);

            }
        },
        error: function () {
            alert(r.msg);
        }
    });
}