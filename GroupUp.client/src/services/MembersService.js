import { accountService } from "./AccountService.js"
import { api } from "./AxiosService.js"

class MembersService {

  async joinGroup(groupId) {
    const res = await api.post('api/members', { groupId })
    await accountService.getMyGroups()
  }
}

export const membersService = new MembersService()