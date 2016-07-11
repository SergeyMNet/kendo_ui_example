//---grid error

function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}

//---Add Document    
var addDoc = function (parentItem) {
    var newItem = makeDocItem();
    var newName = prompt("Create doc " + newItem.text + " ?", newItem.text);

    if (newName != null) {
        $.ajax({
            type: 'POST',
            url: "/Test/AddNewDocument",
            data: { 'newDocName': newName, 'parentName': parentItem },
            dataType: 'json'
        })
        .done(function (data) {
            console.log("AddNewDocument result = " + data.ErrorMessage);
            //$("#kNewTree").data("kendoTreeView").dataSource.read();
            $("#kNewGrid").data("kendoGrid").dataSource.read();
            resultMesssage(data);
        })
    }
};

//--create new doc
function makeDocItem() {
    var txt = kendo.toString(new Date(), "HH_mm_ss");
    return { text: "new_doc_" + txt };
};

function gridEndReqest(data) {
    var resultType = data.type;    
    if (resultType === "update") {
        console.log("reqest end = " + resultType);
        $("#kNewGrid").data("kendoGrid").dataSource.read();
        resultMesssage(data.response);
    } else if (resultType === "destroy") {
        console.log("reqest end = " + resultType);
        resultMesssage(data.response);
    }
}

