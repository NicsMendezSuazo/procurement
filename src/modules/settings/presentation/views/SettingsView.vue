<script setup lang="ts">
import { ref, reactive } from 'vue'
import {
  Settings,
  User,
  Building,
  Bell,
  Shield,
  Database,
  Save,
  Check,
} from 'lucide-vue-next'
import AdminLayout from '../../../shared/layouts/AdminLayout.vue'
import BaseButton from '../../../shared/components/BaseButton.vue'

const activeTab = ref('profile')

const tabs = [
  { key: 'profile', label: 'Profile', icon: User },
  { key: 'organization', label: 'Organization', icon: Building },
  { key: 'notifications', label: 'Notifications', icon: Bell },
  { key: 'security', label: 'Security', icon: Shield },
  { key: 'system', label: 'System', icon: Database },
]

const profile = reactive({
  firstName: 'Maria',
  lastName: 'Santos',
  email: 'm.santos@drmc.doh.gov.ph',
  role: 'BAC Secretariat',
  department: 'Bids and Awards Committee',
  employeeId: 'DRMC-2015-0142',
  contactNumber: '+63 82 555-1234',
})

const organization = reactive({
  agencyName: 'Davao Regional Medical Center',
  agencyCode: 'DRMC',
  region: 'Region XI — Davao Region',
  province: 'Davao del Norte',
  city: 'Tagum City',
  address: 'Apokon, Tagum City, Davao del Norte',
  philgepsCode: 'PHGEPS-A-001234',
})

const notifications = reactive({
  emailNewPO: true,
  emailApproval: true,
  emailRejection: false,
  smsUrgent: true,
  weeklyDigest: false,
})

const security = reactive({
  twoFactor: false,
  sessionTimeout: '30',
  passwordExpiry: '90',
})

const system = reactive({
  currency: 'PHP',
  dateFormat: 'MMM dd, yyyy',
  timezone: 'Asia/Manila (PHT)',
  language: 'English',
})

const saved = ref(false)

function saveSettings() {
  saved.value = true
  setTimeout(() => { saved.value = false }, 2500)
}

const settingsMap: Record<string, typeof profile | typeof organization | typeof notifications | typeof security | typeof system> = {
  profile,
  organization,
  notifications,
  security,
  system,
}
</script>

<template>
  <AdminLayout>
    <div class="max-w-5xl mx-auto">
      <div class="mb-6">
        <div class="flex items-center gap-2 text-sm text-slate-500 mb-1">
          <Settings :size="14" />
          <span class="text-slate-700 font-medium">Settings</span>
        </div>
        <h1 class="text-2xl font-bold text-slate-900">System Settings</h1>
        <p class="text-sm text-slate-500 mt-1">Manage your account, organization, and system preferences</p>
      </div>

      <div class="bg-white rounded-xl border border-slate-200 shadow-sm overflow-hidden">
        <div class="flex flex-col md:flex-row">
          <!-- Tabs Sidebar -->
          <div class="md:w-56 border-b md:border-b-0 md:border-r border-slate-200 bg-slate-50 p-2">
            <nav class="flex md:flex-col gap-1 overflow-x-auto">
              <button
                v-for="tab in tabs"
                :key="tab.key"
                :class="[
                  'flex items-center gap-2.5 px-3 py-2.5 rounded-lg text-sm font-medium whitespace-nowrap transition-colors w-full text-left',
                  activeTab === tab.key
                    ? 'bg-brand-600 text-white'
                    : 'text-slate-600 hover:bg-slate-200',
                ]"
                @click="activeTab = tab.key"
              >
                <component :is="tab.icon" :size="16" />
                {{ tab.label }}
              </button>
            </nav>
          </div>

          <!-- Tab Content -->
          <div class="flex-1 p-6">
            <!-- Profile -->
            <div v-if="activeTab === 'profile'" class="space-y-4">
              <div class="flex items-center gap-4 mb-6">
                <div class="w-16 h-16 rounded-full bg-brand-100 text-brand-700 flex items-center justify-center text-xl font-bold">MS</div>
                <div>
                  <p class="font-semibold text-slate-800">{{ profile.firstName }} {{ profile.lastName }}</p>
                  <p class="text-sm text-slate-500">{{ profile.role }} · {{ profile.department }}</p>
                </div>
              </div>
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div><label class="block text-sm font-medium text-slate-700 mb-1">First Name</label><input v-model="profile.firstName" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Last Name</label><input v-model="profile.lastName" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Email</label><input v-model="profile.email" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Contact Number</label><input v-model="profile.contactNumber" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Role</label><input v-model="profile.role" disabled class="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg bg-slate-50 text-slate-500" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Employee ID</label><input v-model="profile.employeeId" disabled class="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg bg-slate-50 text-slate-500" /></div>
              </div>
            </div>

            <!-- Organization -->
            <div v-else-if="activeTab === 'organization'" class="space-y-4">
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Agency Name</label><input v-model="organization.agencyName" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Agency Code</label><input v-model="organization.agencyCode" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Region</label><input v-model="organization.region" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">PhilGEPS Code</label><input v-model="organization.philgepsCode" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
                <div class="md:col-span-2"><label class="block text-sm font-medium text-slate-700 mb-1">Address</label><input v-model="organization.address" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent" /></div>
              </div>
            </div>

            <!-- Notifications -->
            <div v-else-if="activeTab === 'notifications'" class="space-y-3">
              <label v-for="(val, key) in notifications" :key="key" class="flex items-center justify-between p-3 border border-slate-200 rounded-lg cursor-pointer hover:bg-slate-50 transition-colors">
                <div>
                  <p class="text-sm font-medium text-slate-700 capitalize">{{ key.replace(/([A-Z])/g, ' $1').replace(/^./, c => c.toUpperCase()) }}</p>
                  <p class="text-xs text-slate-400">Receive notifications for this event</p>
                </div>
                <button
                  type="button"
                  :class="['relative inline-flex h-6 w-11 items-center rounded-full transition-colors', val ? 'bg-brand-600' : 'bg-slate-300']"
                  @click="(notifications as Record<string, boolean>)[key] = !val"
                >
                  <span :class="['inline-block h-4 w-4 transform rounded-full bg-white transition-transform', val ? 'translate-x-6' : 'translate-x-1']" />
                </button>
              </label>
            </div>

            <!-- Security -->
            <div v-else-if="activeTab === 'security'" class="space-y-4">
              <div class="flex items-center justify-between p-4 border border-slate-200 rounded-lg">
                <div>
                  <p class="text-sm font-medium text-slate-700">Two-Factor Authentication</p>
                  <p class="text-xs text-slate-400">Add an extra layer of security to your account</p>
                </div>
                <button
                  type="button"
                  :class="['relative inline-flex h-6 w-11 items-center rounded-full transition-colors', security.twoFactor ? 'bg-brand-600' : 'bg-slate-300']"
                  @click="security.twoFactor = !security.twoFactor"
                >
                  <span :class="['inline-block h-4 w-4 transform rounded-full bg-white transition-transform', security.twoFactor ? 'translate-x-6' : 'translate-x-1']" />
                </button>
              </div>
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Session Timeout (minutes)</label>
                  <select v-model="security.sessionTimeout" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent">
                    <option value="15">15 minutes</option><option value="30">30 minutes</option><option value="60">60 minutes</option>
                  </select>
                </div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Password Expiry (days)</label>
                  <select v-model="security.passwordExpiry" class="w-full px-3 py-2 text-sm border border-slate-300 rounded-lg bg-white focus:outline-none focus:ring-2 focus:ring-brand-400 focus:border-transparent">
                    <option value="30">30 days</option><option value="60">60 days</option><option value="90">90 days</option><option value="180">180 days</option>
                  </select>
                </div>
              </div>
            </div>

            <!-- System -->
            <div v-else-if="activeTab === 'system'" class="space-y-4">
              <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Currency</label><input v-model="system.currency" disabled class="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg bg-slate-50 text-slate-500" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Timezone</label><input v-model="system.timezone" disabled class="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg bg-slate-50 text-slate-500" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Date Format</label><input v-model="system.dateFormat" disabled class="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg bg-slate-50 text-slate-500" /></div>
                <div><label class="block text-sm font-medium text-slate-700 mb-1">Language</label><input v-model="system.language" disabled class="w-full px-3 py-2 text-sm border border-slate-200 rounded-lg bg-slate-50 text-slate-500" /></div>
              </div>
              <div class="bg-slate-50 border border-slate-200 rounded-lg p-4 text-sm text-slate-500">
                System-level settings are managed by the IT administrator. Contact the MIS department for changes.
              </div>
            </div>

            <!-- Save Bar -->
            <div class="flex items-center justify-end gap-3 mt-6 pt-4 border-t border-slate-200">
              <transition enter-active-class="transition duration-200" enter-from-class="opacity-0" enter-to-class="opacity-100">
                <span v-if="saved" class="text-sm text-emerald-600 flex items-center gap-1.5 mr-auto">
                  <Check :size="16" /> Settings saved successfully
                </span>
              </transition>
              <BaseButton variant="secondary" @click="() => { /* reset would go here */ }">Reset</BaseButton>
              <BaseButton variant="primary" @click="saveSettings"><Save :size="16" /> Save Changes</BaseButton>
            </div>
          </div>
        </div>
      </div>
    </div>
  </AdminLayout>
</template>
