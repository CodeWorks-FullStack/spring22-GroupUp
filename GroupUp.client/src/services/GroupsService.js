import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class GroupsService {
  async getAll() {
    const res = await api.get('api/groups')
    AppState.groups = res.data
  }
}

export const groupsService = new GroupsService()