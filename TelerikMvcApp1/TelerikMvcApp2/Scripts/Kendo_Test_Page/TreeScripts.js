//-----------------Add-Remmove Folders

//---Add Folder Next
var addAfter = function (parentItem) {
    var newItem = makeItem();

    $.ajax({
        type: 'POST',
        url: "/Test/AddNewNextCategory",
        data: { 'newItemName': newItem.text, 'parentName': parentItem },
        dataType: 'json'
    })
    .done(function (data) {
        console.log("AddNewAfterCategory result = " + data);
        $("#kNewTree").data("kendoTreeView").dataSource.read();
        $("#Category").data("kendoDropDownList");
        resultMesssage(data);
    })

};

//---Add Folder New
var addNewFolder = function () {
    var newItem = makeItem();

    $.ajax({
        type: 'POST',
        url: "/Test/AddNewNextCategory",
        data: { 'newItemName': newItem.text },
        dataType: 'json'
    })
    .done(function (data) {
        console.log("AddNewAfterCategory result = " + data);
        $("#kNewTree").data("kendoTreeView").dataSource.read();
        $("#Category").data("kendoDropDownList");
        resultMesssage(data);
    })

};

//---Add Folder Child
var addChild = function (parentItem) {
    var newItem = makeItem();

    $.ajax({
        type: 'POST',
        url: "/Test/AddNewChildCategory",
        data: { 'newItemName': newItem.text, 'parentName': parentItem },
        dataType: 'json'
    })
      .done(function (data) {
          console.log("AddNewChildCategory result = " + data);
          $("#kNewTree").data("kendoTreeView").dataSource.read();
          $("#Category").data("kendoDropDownList");
          resultMesssage(data);
      });
};

//---Remove Folder
var remove = function (item) {

    $.ajax({
        type: 'POST',
        url: "/Test/RemoveCategory",
        data: { 'targetItemName': item },
        dataType: 'json'
    })
      .done(function (data) {
          console.log("RemoveCategory result = " + data);          
          $("#Category").data("kendoDropDownList");
          $("#kNewTree").data("kendoTreeView").dataSource.read();
          $("#kNewGrid").data("kendoGrid").dataSource.read();
          resultMesssage(data);
      });
};



//--create new folderTemplate
function makeItem() {
    var txt = kendo.toString(new Date(), "HH:mm:ss");
    return { text: "new_folder_" + txt };
};

function editNameFolder(targItem) {
    var newName = prompt("Edit folder " + targItem + " ?", targItem);

    $.ajax({
        type: 'POST',
        url: "/Test/EditNameFolder",
        data: { 'oldName': targItem, 'newName': newName },
        dataType: 'json'
    })
     .done(function (data) {
         console.log("ChangeName result = " + data.ErrorMessage);
         $("#kNewTree").data("kendoTreeView").dataSource.read();
         $("#Category").data("kendoDropDownList");
         resultMesssage(data);
     });

};


//-----------------

var targetItem = "";
var targetFilterStatus = "";
var targetFilterDate = "";
var targetFilterName = "";

function AddDataTreeFilter() {
    return {
        targetCategory: targetItem,
        filterStatus: targetFilterStatus,
        filterDate: targetFilterDate,
        filterName: targetFilterName
    }
}

function OnTreeSelect(e) {
    targetItem = "";
    targetItem = e.node.firstChild.innerText;
    console.log("targetItem = " + targetItem);
    $("#kNewGrid").data("kendoGrid").dataSource.read();

}

function Drop(e) {
    if (e.statusClass == "denied") {
        return;
    } else {
        //console.log("e = " + e);
        if (e.statusClass == "add") {
            e.setStatusClass("k-denied");
        }
    }
}

function OnTreeDrop(e) {
    var childid = e.sourceNode.firstChild.innerText;
    var parentid = e.destinationNode.firstChild.innerText;
    var position = e.dropPosition;

    console.log("childid = " + childid);
    console.log("parentid = " + parentid);

    var askResult = confirm("Drop " + childid + " to " + position + " " + parentid + " ?");
    //console.log(askResult);
    if (askResult) {
        $.ajax({
            type: 'POST',
            url: "/Test/SaveNodeTree",
            data: { 'childid': childid, 'parentid': parentid, 'position': position },
            dataType: 'json'
        })
            .done(function (data) {
                //console.log("Drop result = " + data);
                resultMesssage(data);
            });
    } else {
        console.log("Drop cancel");
        $("#kNewTree").data("kendoTreeView").dataSource.read();
    }
}
