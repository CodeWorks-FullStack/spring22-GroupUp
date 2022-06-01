import { AppState } from "../AppState.js"
import { api } from "./AxiosService.js"

class GroupsService {
  async getAll() {
    const res = await api.get('api/groups')
    AppState.groups = res.data
  }

  async getById(id) {
    const res = await api.get(`api/groups/${id}`)
    AppState.activeGroup = res.data
  }
}

export const groupsService = new GroupsService()