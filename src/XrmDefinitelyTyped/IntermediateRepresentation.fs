﻿module DG.XrmDefinitelyTyped.IntermediateRepresentation

type Option = {
  label: string 
  value: int
}

type OptionSet = {
  displayName: string
  options: Option[]
}

type SpecialType = 
  | Default 
  | OptionSet 
  | Money 
  | Guid 
  | EntityReference
  | Decimal

type XrmAttribute = { 
  schemaName: string
  logicalName: string
  varType: TsType
  specialType: SpecialType
  targetEntitySets: (string * string)[] option
  readable: bool
  createable: bool
  updateable: bool
}

type XrmRelationship = {
  schemaName: string
  attributeName: string
  relatedSetName: string
  relatedSchemaName: string
  navProp: string
  referencing: bool
}

type XrmEntity = {
  typecode: int
  schemaName: string
  logicalName: string
  entitySetName: string option
  idAttribute: string
  attributes: XrmAttribute list 
  optionSets: OptionSet list
  availableRelationships: XrmRelationship list
  allRelationships: XrmRelationship list
  relatedEntities: string list 
}

type XrmView = {
  name: string;
  entityName: EntityName;
  attributes: XrmAttribute list
  linkedAttributes: XrmAttribute list
}

type XrmActionParameter = {
  name: string;
  varType: TsType;
  optional: bool;
  isOutputParameter: bool;
}

type XrmAction = {
  name: string;
  boundEntity: string option;
  parameters : XrmActionParameter list;
}

// Forms
type ControlType = 
  | Default
  | Number
  | Date
  | Lookup of string
  | OptionSet
  | SubGrid of string
  | WebResource
  | IFrame

type AttributeType = 
  | Default of TsType
  | Number
  | Lookup of string
  | Date
  | OptionSet of TsType

type FormType =
  | Dashboard = 0
  | AppointmentBook = 1
  | Main = 2
  | MiniCampaignBO = 3
  | Preview = 4
  | Mobile = 5
  | Quick = 6
  | QuickCreate = 7
  | Dialog = 8
  | TaskFlowForm = 9
  | InteractionCentricDashboard = 10
  | Card = 11
  | MainInteractionCentric = 12
  | Other = 100
  | MainBackup = 101
  | AppointmentBookBackup = 102

type XrmFormAttribute = string * AttributeType
type XrmFormControl = string * AttributeType option * ControlType
type XrmFormTab = string * string * string list
  
type ControlClassId =
  | CheckBox | DateTime | Decimal | Duration | EmailAddress | EmailBody 
  | Float | IFrame | Integer | Language | Lookup | MoneyValue | Notes
  | PartyListLookup | Picklist | RadioButtons | RegardingLookup 
  | StatusReason | TextArea | TextBox | TickerSymbol | TimeZonePicklist | Url
  | WebResource | Map | Subgrid | QuickView | Timer
  | Other
  with override x.ToString() = x.GetType().Name

type ControlField = string * string * ControlClassId * string option


type XrmForm = {
  entityName: string
  entityDependencies: string seq
  guid: System.Guid option
  formType: string option
  name: string
  attributes: XrmFormAttribute list
  controls: XrmFormControl list
  tabs: XrmFormTab list
}



type InterpretedState = {
  outputDir: string
  entities: XrmEntity[]
  forms: XrmForm[]
  bpfControls: Map<string,ControlField list>
  imageWebResourceNames: string[]
  lcidData: int[]
  viewData: XrmView[]
  actionData: XrmAction[]
}
