

function validateFloatKeyPress(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode !== 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot (thanks ddlab)
    if (number.length > 1 && charCode === 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}

function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text === '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}

function checkFloatNumber(el) {
    var v = parseFloat(el.value);
    el.value = (isNaN(v)) ? '' : v.toFixed(2);
}

function showNextAgent() {   
    if ($("#agent2").is(":hidden")) {
        $("#agent2").show();
    } else if ($("#agent3").is(":hidden")) {
        $("#agent3").show();
    } else if ($("#agent4").is(":hidden")) {
        $("#agent4").show();
    } else if ($("#agent5").is(":hidden")) {
        $("#agent5").show();
        $('#btnShowNextAgent').hide();
    }

    if ($("#agent2").is(":visible") && $("#agent3").is(":visible") && $("#agent4").is(":visible") && $("#agent5").is(":visible")) {
        $('#btnShowNextAgent').hide();
    }
}

function removeAgent(idx) {
    
    if (idx === 'agent2') {
        $('#Agent2').val('');
        $('#prop2').val('');
        
    } else if (idx === 'agent3') {
        $('#Agent3').val('');
        $('#prop3').val('');
    } else if (idx === 'agent4') {
        $('#Agent4').val('');
        $('#prop4').val('');
    } else if (idx === 'agent5') {
        $('#Agent5').val('');
        $('#prop5').val('');
    }

    $("#" + idx).hide();
    $('#btnShowNextAgent').show();
}


function validateLeadData() {
   
    var leadDataValid = true;
    var pc = $('#ddlPC').val();

    if (pc === "C9CE6B9A-771F-4ED7-9CEE-6B8F131E4DA5".toLowerCase() || pc === "FCF9DDF7-BF80-4503-9C5E-CECF428491EC".toLowerCase()) {
        //third party or motor
        if ($('#txtMakeOfVehicle').val() === '' || $('#txtMakeOfVehicle').val() === undefined || $('#txtMakeOfVehicle').val() === null) {
            leadDataValid = false;
            $('#txtMakeOfVehicle').addClass('error');
        } else { $('#txtMakeOfVehicle').removeClass('error'); }
        if ($('#txtModelOfVehicle').val() === '' || $('#txtModelOfVehicle').val() === undefined || $('#txtModelOfVehicle').val() === null) {
            leadDataValid = false;
            $('#txtModelOfVehicle').addClass('error');
        } else { $('#txtModelOfVehicle').removeClass('error'); }
        if ($('#txtYearOfMake').val() === '' || $('#txtYearOfMake').val() === undefined || $('#txtYearOfMake').val() === null) {
            leadDataValid = false;
            $('#txtYearOfMake').addClass('error');
        } else { $('#txtYearOfMake').removeClass('error'); }
        if ($('#txtValueInsured').val() === '' || $('#txtValueInsured').val() === undefined || $('#txtValueInsured').val() === null) {
            leadDataValid = false;
            $('#txtValueInsured').addClass('error');
        } else { $('#txtValueInsured').removeClass('error'); }

    } else if (pc === "8ED74480-CA28-418A-ADD6-FE25E8E15B86".toLowerCase() || pc === "D2CFA791-27FC-4CEF-970E-9251FC92D120".toLowerCase()) {

        if ($('#txtNameOfAssured').val() === '' || $('#txtNameOfAssured').val() === undefined || $('#txtNameOfAssured').val() === null) {
            leadDataValid = false;
            $('#txtNameOfAssured').addClass('error');
        } else {
            $('#txtNameOfAssured').removeClass('error');
        }
        if ($('#idDOB').val() === '' || $('#idDOB').val() === undefined || $('#idDOB').val() === null) {
            leadDataValid = false;
            $('#idDOB').addClass('error');
        } else {
            $('#idDOB').removeClass('error');
        }

        if ($('#txtSumAssessed').val() === '' || $('#txtSumAssessed').val() === undefined || $('#txtSumAssessed').val() === null) {
            leadDataValid = false;
            $('#txtSumAssessed').addClass('error');
        } else {
            $('#txtSumAssessed').removeClass('error');
        }
    } else if (pc === "E56249E3-E821-4969-80C4-B97BD4798CC2".toLowerCase()) {
        if ($('#txtNatureOfContract').val() === '' || $('#txtNatureOfContract').val() === undefined || $('#txtNatureOfContract').val() === null) {
            leadDataValid = false;
            $('#txtNatureOfContract').addClass('error');
        } else {
            $('#txtNatureOfContract').removeClass('error');
        }

        if ($('#txtProjectName').val() === '' || $('#txtProjectName').val() === undefined || $('#txtProjectName').val() === null) {
            leadDataValid = false;
            $('#txtProjectName').addClass('error');
        } else {
            $('#txtProjectName').removeClass('error');
        }
        if ($('#txtContractAmount').val() === '' || $('#txtContractAmount').val() === undefined || $('#txtContractAmount').val() === null) {
            leadDataValid = false;
            $('#txtContractAmount').addClass('error');
        } else {
            $('#txtContractAmount').removeClass('error');
        }
        if ($('#txtBondAmount').val() === '' || $('#txtBondAmount').val() === undefined || $('#txtBondAmount').val() === null) {
            leadDataValid = false;
            $('#txtBondAmount').addClass('error');
        } else {
            $('#txtBondAmount').removeClass('error');
        }

    }

    if ($('#idStartDate').val() === '' || $('#idStartDate').val() === undefined || $('#idStartDate').val() === null) {
        leadDataValid = false;
        $('#idStartDate').addClass('error');
    } else { $('#idStartDate').removeClass('error'); }
    if ($('#idEndDate').val() === '' || $('#idEndDate').val() === undefined || $('#idEndDate').val() === null) {
        leadDataValid = false;
        $('#idEndDate').addClass('error');
    } else {
        $('#idEndDate').removeClass('error');
    }
    if ($('#txtSumInsured').val() === '' || $('#txtSumInsured').val() === undefined || $('#txtSumInsured').val() === null) {
        leadDataValid = false;
        $('#txtSumInsured').addClass('error');
    } else { $('#txtSumInsured').removeClass('error'); }
    if ($('#txtNetPremium').val() === '' || $('#txtNetPremium').val() === undefined || $('#txtNetPremium').val() === null) {
        leadDataValid = false;

        $('#txtNetPremium').addClass('error');
    } else {

        $('#txtNetPremium').removeClass('error');
    }

    if ($('#txtCommission').val() === '' || $('#txtCommission').val() === undefined || $('#txtCommission').val() === null) {
        leadDataValid = false;

        $('#txtCommission').addClass('error');
    } else {

        $('#txtCommission').removeClass('error');
    }
    if ($('#txtGP').val() === '' || $('#txtGP').val() === undefined || $('#txtGP').val() === null) {
        leadDataValid = false;

        $('#txtGP').addClass('error');
    } else {

        $('#txtGP').removeClass('error');
    }

    if (($('#idEndDate').val() !== '' && $('#idEndDate').val() !== undefined && $('#idEndDate').val() !== null) && ($('#idStartDate').val() !== '' && $('#idStartDate').val() !== undefined && $('#idStartDate').val() !== null)) {

        var stDate = $('#idStartDate').val();
        var eDate = $('#idEndDate').val();
        if (new Date(eDate) <= new Date(stDate)) {
            kendo.alert('End date should be greater then start date');
            leadDataValid = false;
        }
    }

    if (!leadDataValid) {
        
        kendo.alert('Please provide the required data');
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
        //    kendo.alert('Gross premium cannot be more than sum insured');
        //    leadDataValid = false;
        //    return;
        //}
    }

    if ((grossP_val !== "" && grossP_val !== null && grossP_val !== undefined) && (comm_val !== "" && comm_val !== null && comm_val !== undefined)) {

        grossP_val = parseFloat(grossP_val);
        comm_val = parseFloat(comm_val);
        if (comm_val > grossP_val) {
            kendo.alert('Commission cannot be more than gross premium');
            leadDataValid = false;
            return;
        }
    }

    leadDataValid = checkAgentsProportion();
    
    if (leadDataValid) {
         $('#idLeadSubmit').click();
        //kendo.alert('pass');
    }
    else {
        // alert('Please provide the required data');
    }
}


function cancelAddLead() {
    $('#noticeModal23').modal('hide');
    $('input').attr('readonly', false);
    $('select').attr('readonly', false);

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
                    words.push(and);
                }

            }

            /* Add hundreds word if array item exists */
            if ((word = units[ints[2]])) {
                words.push(word + ' hundred');
            }

        }

    }

    return words.reverse().join(' ');

}


function checkAgentsProportion() {
    debugger;
    var prop1 = $('#prop1').val();
    var prop2 = $('#prop2').val();
    var prop3 = $('#prop3').val();
    var prop4 = $('#prop4').val();
    var prop5 = $('#prop5').val();

    var totalVal = 0;
    if (!isNaN(prop1) && prop1 !== undefined && prop1 !== '') {
        totalVal = parseFloat(totalVal) + parseFloat(prop1);
    }

    if (!isNaN(prop2) && prop2 !== undefined && prop2!=='') {
        totalVal = parseFloat(totalVal) + parseFloat(prop2);
    }

    if (!isNaN(prop3) && prop3 !== undefined && prop3 !== '') {
        totalVal = parseFloat(totalVal) + parseFloat(prop3);
    }

    if (!isNaN(prop4) && prop4 !== undefined && prop4 !== '') {
        totalVal = parseFloat(totalVal) + parseFloat(prop4);
    }

    if (!isNaN(prop5) && prop5 !== undefined && prop5 !== '') {
        totalVal = parseFloat(totalVal) + parseFloat(prop5);
    }

    if (totalVal > parseFloat(100)) {
        kendo.alert('Total agents proportion should not be greater then 100.');
        return false;
    }
    return true;
}