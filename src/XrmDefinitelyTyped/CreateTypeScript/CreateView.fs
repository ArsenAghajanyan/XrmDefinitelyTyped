﻿module internal DG.XrmDefinitelyTyped.CreateView

open DG.XrmDefinitelyTyped.IntermediateRepresentation

let varSpecialTypeConv list =
  list 
  |> List.collect (fun v -> 
    let vType = 
      match v.specialType with
      | SpecialType.OptionSet       -> TsType.SpecificGeneric("SDK.OptionSet", [v.varType])
      | SpecialType.Money           -> TsType.Custom "SDK.Money"
      | SpecialType.EntityReference -> TsType.Custom "SDK.EntityReference"
      | SpecialType.Decimal         -> TsType.String
      | _ -> v.varType
    let valueName = sprintf "%s%s" v.logicalName "_Value"
    [Variable.Create(v.logicalName, TsType.String); Variable.Create(valueName, vType)]
  )

let getVars ownedAttributes linkedAttributes =
  ownedAttributes @ linkedAttributes
  |> varSpecialTypeConv

let getViewInterface ns (view: XrmView) =
  let is = Interface.Create(view.name, vars = (getVars view.attributes view.linkedAttributes))
  
  let ns = Namespace.Create((sprintf "%s.%s" ns view.entityName), declare = true, interfaces = [is])
  CreateCommon.skipNsIfEmpty ns