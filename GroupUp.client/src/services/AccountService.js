import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyGroups() {
    try {
      const res = await api.get('/account/groups')
      AppState.myGroups = res.data
    } catch (error) {
      logger.error('MyGroups Failed:', err)
    }
  }
}

export const accountService = new AccountService()
