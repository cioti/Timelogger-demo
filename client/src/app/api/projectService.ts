import { ApiClient } from "./apiClient"
import { ProjectFormData, Project, ApiResponse } from "../interfaces/interfaces"

export const projectService = {
    createAsync: async function (formData: ProjectFormData): Promise<ApiResponse<string> | null> {
        return await ApiClient.sendRequestAsync<ProjectFormData, ApiResponse<string>>("/api/v1/projects", "POST", formData);
    },
    listAsync: async function (): Promise<ApiResponse<Array<Project>> | null> {
        return await ApiClient.sendRequestAsync<null, ApiResponse<Array<Project>>>("/api/v1/projects", "GET", null);
    }
}
