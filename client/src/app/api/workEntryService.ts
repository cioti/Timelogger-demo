import { ApiClient } from "./apiClient"
import { WorkEntryFormData, WorkEntry, ApiResponse } from "../interfaces/interfaces"

export const workEntryService = {
    createAsync: async function (projectId: string, formData: WorkEntryFormData): Promise<ApiResponse<number> | null> {
        return await ApiClient.sendRequestAsync<WorkEntryFormData, ApiResponse<number>>(`/api/v1/projects/${projectId}/work-entries`, "POST", formData);
    },
    listAsync: async function (projectId: string): Promise<ApiResponse<Array<WorkEntry>> | null> {
        return await ApiClient.sendRequestAsync<null, ApiResponse<Array<WorkEntry>>>(`/api/v1/projects/${projectId}/work-entries`, "GET", null);
    }
}
