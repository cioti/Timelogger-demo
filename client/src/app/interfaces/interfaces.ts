export interface ProjectFormData {
    name: string,
    description: string,
    startDate: Date,
    endDate: Date,
    clientFirstname: string,
    clientLastname: string
}

export interface Project {
    id: string,
    name: string,
    description: string,
    startDate: Date,
    endDate: Date,
    clientFirstname: string,
    clientLastname: string
}
export interface WorkEntry {
    id: string,
    title: string,
    description: string,
    hours: number,
    date: Date,
}
export interface WorkEntryFormData {
    title: string,
    description: string,
    hours: number,
    date: Date,
}
export interface ApiResponse<T> {
    result: T,
    message: string,
    apiVersion: string
}