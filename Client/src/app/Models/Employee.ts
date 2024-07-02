export interface Employee {
    id:string
    fullName : String
    userName : String
    email : String 
    phone : String
    password : String
    branchId : number | null
    branchName : string | null
    roles : string[] | null
    status : boolean

}