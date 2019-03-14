
function ULS_SP() {
    if (ULS_SP.caller) {
        ULS_SP.caller.ULSTeamName = "Windows SharePoint Services 4";
        ULS_SP.caller.ULSFileName = "/_layouts/15/MAPPS/Scripts/PageComponent.js";
    }
}


Type.registerNamespace('MAPPS');

// RibbonApp Page Component
MAPPS.PageComponent = function () {
    ULS_SP();
    MAPPS.PageComponent.initializeBase(this);
}


MAPPS.PageComponent.initialize = function () {
    ULS_SP();
    //ExecuteOrDelayUntilScriptLoaded(Function.createDelegate(null, MAPPS.PageComponent.initializePageComponent), 'SP.Ribbon.js');
    _spBodyOnLoadFunctionNames.push("MAPPS.PageComponent.initializePageComponent");
}


MAPPS.PageComponent.initializePageComponent = function () {
    ULS_SP();
    var ribbonPageManager = SP.Ribbon.PageManager.get_instance();
    if (null !== ribbonPageManager) {
        ribbonPageManager.addPageComponent(MAPPS.PageComponent.instance);
        ribbonPageManager.get_focusManager().requestFocusForComponent(
    MAPPS.PageComponent.instance);
    }
}


MAPPS.PageComponent.refreshRibbonStatus = function () {
    SP.Ribbon.PageManager.get_instance().get_commandDispatcher().executeCommand(
  Commands.CommandIds.ApplicationStateChanged, null);
}


MAPPS.PageComponent.prototype = {
    getFocusedCommands: function () {
        ULS_SP();
        return [];
    },
    getGlobalCommands: function () {
        ULS_SP();
        return getGlobalCommands();
    },
    isFocusable: function () {
        ULS_SP();
        return true;
    },
    receiveFocus: function () {
        ULS_SP();
        return true;
    },
    yieldFocus: function () {
        ULS_SP();
        return true;
    },
    canHandleCommand: function (commandId) {
        ULS_SP();
        return commandEnabled(commandId);
    },
    handleCommand: function (commandId, properties, sequence) {
        ULS_SP();
        return handleCommand(commandId, properties, sequence);
    }
}

// Register classes
MAPPS.PageComponent.registerClass('MAPPS.PageComponent', CUI.Page.PageComponent);
MAPPS.PageComponent.instance = new MAPPS.PageComponent();

// Notify waiting jobs
NotifyScriptLoadedAndExecuteWaitingJobs("/_layouts/15/MAPPS/Scripts/PageComponent.js");
